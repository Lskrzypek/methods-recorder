using System;

namespace MethodsRecorder.Writters
{
    public class DateTimeFileNameGenerator : IFileNameGenerator
    {
        private const string Extension = "txt";

        public string GenerateFileName(MethodData data, Package package)
        {
            var packageName = package.Name ?? "MethodsRecorder";
            var date = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss");
            return $"{packageName}_{date}.{Extension}";
        }
    }
}