namespace MethodsRecorder.Writters
{
    public class NameBasedFileNameGenerator : IFileNameGenerator
    {
        private const string Extension = "txt";
        private readonly string FileName;
        private int FileNumber = 1;

        public NameBasedFileNameGenerator(string fileName)
        {
            FileName = fileName;
        }

        public string GenerateFileName(MethodData data, Package package)
        {
            if (FileNumber == 1)
            {
                FileNumber++;
                return $"{FileName}.{Extension}";
            }

            return $"{FileName}_{FileNumber++}.{Extension}";
        }
    }
}