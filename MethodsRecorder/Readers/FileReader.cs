﻿using System;
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
        private readonly ObjectComparator objectComparator = new ObjectComparator();
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
                .Where(x => x.MethodName == methodName && AreArgumentsEquals(arguments, x.Arguments))
                .FirstOrDefault();

            var returnValue = DeserializeObject(md.ReturnValue, returnType);
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

        private bool AreArgumentsEquals(object[] methodArguments, object[] fileArguments)
        {
            if (methodArguments.Length != fileArguments.Length)
                return false;

            if(methodArguments.Length == 0)
                return true;

            var fileDeserializedArguments = fileArguments
                .Select((obj, index) => DeserializeObject(obj, methodArguments[index].GetType()))
                .ToArray();

            return !methodArguments
                .Select((x, index) => (x, index))
                .Any(x => !objectComparator.Compare(x.x, fileDeserializedArguments[x.index]));
        }

        private object DeserializeObject(object obj, Type expectedType)
        {
            var jsonElement = (JsonElement)obj;
            return JsonSerializer.Deserialize(jsonElement.GetRawText(), expectedType);
        }
    }
}
