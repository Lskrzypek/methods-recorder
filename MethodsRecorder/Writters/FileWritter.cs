using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MethodsRecorder.Writters
{
    public class FileWritter : IWritter
    {
        private readonly string FilePath;
        private readonly TaskQueue TaskQueue;

        private int OrderNumber;

        public FileWritter(string filePath)
        {
            FilePath = filePath;
            TaskQueue = new TaskQueue();
        }

        public void WaitToCompleteWrite()
        {
            TaskQueue.WaitForAllTasks().Wait();
        }

        public async void Write(MethodData data)
        {
            await TaskQueue.Enqueue(() => WriteToFile(data));
        }

        private async Task WriteToFile(MethodData data)
        {
            bool isFirst = OrderNumber == 0;

            if (isFirst)
            {
                DeleteFile();
                OrderNumber = 1;
            }

            using (var sw = new StreamWriter(FilePath, true, Encoding.UTF8))
            {
                data.OrderNumber = OrderNumber;

                sw.Write(SerializeMethod(data));
                sw.WriteLine(",");

                OrderNumber++;
            }

            await Task.CompletedTask;
        }

        private void DeleteFile()
        {
            if(File.Exists(FilePath))
                File.Delete(FilePath);
        }

        private string SerializeMethod<T>(T data)
        {
            return JsonSerializer.Serialize(data);
        }
    }
}