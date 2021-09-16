using System.IO;
using System.Text;
using System.Text.Json;

namespace MethodsRecorder.Writters
{
    public class FileWritter : IWritter
    {
        public Package Package { get; set; }

        private readonly string DirectoryPath;
        private readonly IFileNameGenerator FileNameGenerator;

        private string FullFileName;

        private bool HaveWrittenRecords = false;

        public FileWritter(string directoryPath)
        {
            DirectoryPath = directoryPath;
            FileNameGenerator = new DateTimeFileNameGenerator();
        }

        public FileWritter(string directoryPath, IFileNameGenerator fileNameGenerator)
        {
            DirectoryPath = directoryPath;
            FileNameGenerator = fileNameGenerator;
        }

        public void Write(MethodData data)
        {
            if (IsFirstRecordingMethod(data))
            {
                SetFileName(data);
            }

            WriteFile(data);

            HaveWrittenRecords = true;
        }

        public void Init()
        {
            HaveWrittenRecords = false;
        }

        public void Complete()
        {
        }

        private bool IsFirstRecordingMethod(MethodData data)
        {
            return !HaveWrittenRecords;
        }

        private void SetFileName(MethodData data)
        {
            FullFileName = Path.Combine(DirectoryPath, FileNameGenerator.GenerateFileName(data, Package));
        }

        private void WriteFile(MethodData data)
        {
            using var sw = new StreamWriter(FullFileName, true, Encoding.UTF8);
            sw.Write(SerializeMethod(data));
            sw.WriteLine(",");
        }

        private static string SerializeMethod<T>(T data)
        {
            return JsonSerializer.Serialize(data);
        }
    }
}