using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MethodsRecorder.Writters
{
    internal class TaskQueue
    {
        private SemaphoreSlim semaphore;
        private List<Guid> taskGuids = new List<Guid>();

        public TaskQueue()
        {
            semaphore = new SemaphoreSlim(1);
        }

        public async Task<T> Enqueue<T>(Func<Task<T>> task)
        {
            var guid = Guid.NewGuid();
            taskGuids.Add(guid);

            await semaphore.WaitAsync();

            try
            {
                return await task();
            }
            finally
            {
                taskGuids.Remove(guid);
                semaphore.Release();
            }
        }

        public async Task Enqueue(Func<Task> task)
        {
            var guid = Guid.NewGuid();
            taskGuids.Add(guid);

            await semaphore.WaitAsync();

            try
            {
                await task();
            }
            finally
            {
                taskGuids.Remove(guid);
                semaphore.Release();
            }
        }

        public async Task WaitForAllTasks()
        {
            while(true)
            {
                try
                {
                    await semaphore.WaitAsync();

                    if (taskGuids.Count == 0)
                        break;

                    await Task.Delay(1);
                }
                finally
                {
                    semaphore.Release();
                }
            }
        }
    }
}