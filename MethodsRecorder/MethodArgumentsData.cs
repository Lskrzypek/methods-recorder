using System.Text.Json;

namespace MethodsRecorder
{
    internal class MethodArgumentsData : IDataToWrite
    {
        public string Data { get; private set; }
        public string Header => "#Arguments";
        public string MethodName { get; private set; }

        public MethodArgumentsData(object[] args, string methodName)
        {
            Data = GetStringFromArguments(args);
            MethodName = methodName;
        }

        public static string GetStringFromArguments(object[] args)
        {
            return JsonSerializer.Serialize(args);
        }
    }
}