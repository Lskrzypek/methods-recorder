using System;

namespace MethodsRecorder.Writters
{
    internal class DateTimeFileNameGenerator : IFileNameGenerator
    {
        private const string Extension = "txt";

        public string GenerateFileName(MethodData data)
        {
            var date = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            return $"{data.Package.Name}_{date}.{Extension}";
        }
    }
}