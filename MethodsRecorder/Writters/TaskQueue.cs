using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MethodsRecorder.Writters
{
    internal class TaskQueue
    {
        private readonly SemaphoreSlim Semaphore;
        private readonly List<Guid> TaskGuids = new ();

        public TaskQueue()
        {
            Semaphore = new SemaphoreSlim(1);
        }

        public async Task<T> Enqueue<T>(Func<Task<T>> task)
        {
            var guid = Guid.NewGuid();
            TaskGuids.Add(guid);

            await Semaphore.WaitAsync();

            try
            {
                return await task();
            }
            finally
            {
                TaskGuids.Remove(guid);
                Semaphore.Release();
            }
        }

        public async Task Enqueue(Func<Task> task)
        {
            var guid = Guid.NewGuid();
            TaskGuids.Add(guid);

            await Semaphore.WaitAsync();

            try
            {
                await task();
            }
            finally
            {
                TaskGuids.Remove(guid);
                Semaphore.Release();
            }
        }

        public async Task WaitForAllTasks()
        {
            while(true)
            {
                try
                {
                    await Semaphore.WaitAsync();

                    if (TaskGuids.Count == 0)
                        break;

                    await Task.Delay(1);
                }
                finally
                {
                    Semaphore.Release();
                }
            }
        }
    }
}