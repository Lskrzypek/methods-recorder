using System.IO;
using System.Text;
using System.Text.Json;

namespace MethodsRecorder.Writters
{
    internal class FileWritter : IWritter
    {
        private readonly string FilePath;

        private int OrderNumber;

        public FileWritter(string filePath)
        {
            FilePath = filePath;
        }

        public void Write(MethodData data)
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