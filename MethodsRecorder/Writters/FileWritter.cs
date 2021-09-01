using System.IO;
using System.Text;
using System.Text.Json;

namespace MethodsRecorder.Writters
{
    public class FileWritter : IWritter
    {
        private readonly string FilePath;

        public FileWritter(string filePath)
        {
            FilePath = filePath;
        }

        public void Write(MethodData data)
        {
            bool isFirst = data.OrderNumber == 0;

            if (isFirst)
            {
                DeleteFile();
            }

            using var sw = new StreamWriter(FilePath, true, Encoding.UTF8);
            sw.Write(SerializeMethod(data));
            sw.WriteLine(",");
        }

        private void DeleteFile()
        {
            if (File.Exists(FilePath))
                File.Delete(FilePath);
        }

        private static string SerializeMethod<T>(T data)
        {
            return JsonSerializer.Serialize(data);
        }
    }
}