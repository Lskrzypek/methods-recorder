using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.Json;

namespace MethodsRecorder.Readers
{
    internal class FileReader : IReader
    {
        private readonly string FilePath;
        private List<MethodData> MethodsData;

        public FileReader(string filePath)
        {
            FilePath = filePath;
        }

        public object ReadMethod(string methodName, object[] arguments, Type returnType)
        {
            if (MethodsData == null)
                ReadAllFileData();

            var md = MethodsData
                .Where(x => x.MethodName == methodName && x.AreArgumentsEquals(arguments))
                .FirstOrDefault();

            var returnElement = (JsonElement)md.ReturnValue;
            var returnValue = JsonSerializer.Deserialize(returnElement.GetRawText(), returnType);
            return returnValue;
        }

        private void ReadAllFileData()
        {
            using (var sr = new StreamReader(FilePath, Encoding.UTF8))
            {
                var fileStr = sr.ReadToEnd();
                MethodsData = ParseTextToMethodsData(fileStr).ToList();
            }
        }

        private IEnumerable<MethodData> ParseTextToMethodsData(string text)
        {
            var textToDeserialize = "[" + text.Substring(0, text.Length - 3) + "]";
            var methodsList = JsonSerializer.Deserialize<IEnumerable<MethodData>>(textToDeserialize);
            return methodsList;
        }
    }
}
