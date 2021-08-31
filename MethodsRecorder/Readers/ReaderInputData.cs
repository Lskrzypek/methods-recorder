using System;

namespace MethodsRecorder.Readers
{
    public class ReaderInputData
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public object[] Arguments { get; set; }
        public Type ReturnType { get; set; }
    }
}