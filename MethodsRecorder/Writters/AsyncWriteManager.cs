using System.Threading.Tasks;

namespace MethodsRecorder.Writters
{
    internal class AsyncWriteManager : IWriteManager
    {
        public int CurrentRecordNumber { get; private set; }

        private readonly TaskQueue TaskQueue;
        private readonly IWritter Writter;

        public AsyncWriteManager(IWritter writter)
        {
            TaskQueue = new TaskQueue();
            Writter = writter;
        }

        public async void Write(MethodData data)
        {
            CurrentRecordNumber++;
            await TaskQueue.Enqueue(() => DoWrite(data));
        }

        public void CompleteWrite()
        {
            TaskQueue.WaitForAllTasks().Wait();
        }

        private async Task DoWrite(MethodData data)
        {
            Writter.Write(data);

            await Task.CompletedTask;
        }
    }
}