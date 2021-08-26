using System.Text.Json;

namespace MethodsRecorder
{
    internal class MethodArgumentsData : IDataToWrite
    {
        public string Data { get; private set; }

        public string Header => "#Arguments";

        public MethodArgumentsData(object[] args)
        {
            Data = GetStringFromArguments(args);
        }

        public static string GetStringFromArguments(object[] args)
        {
            return JsonSerializer.Serialize(args);
        }
    }
}