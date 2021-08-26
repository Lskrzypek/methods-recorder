using System.IO;
using System.Text;

namespace MethodsRecorder.Writters
{
    internal class FileWritter : IWritter
    {
        private readonly string FilePath;

        private int Id;

        public FileWritter(string filePath)
        {
            FilePath = filePath;
        }

        public void Write(IDataToWrite data)
        {
            if (Id == 0)
            {
                DeleteFile();
                Id = 1;
            }

            using (var sw = new StreamWriter(FilePath, true, Encoding.UTF8))
            {
                sw.WriteLine($"#METHOD_{Id}:");
                sw.WriteLine(data.Header);
                sw.WriteLine(data.Data);
                sw.WriteLine();
            }
        }

        public void NextMethod()
        {
            Id++;
        }

        private void DeleteFile()
        {
            if(File.Exists(FilePath))
                File.Delete(FilePath);
        }
    }
}