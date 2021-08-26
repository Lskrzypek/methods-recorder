using System.Text.Json;

namespace MethodsRecorder
{
    public class MethodReturnValueData : IDataToWrite
    {
        public string Data { get; private set; }
        public string Header => "#ReturnValue";
        public string MethodName { get; private set; }

        public MethodReturnValueData(object returnValue, string methodName)
        {
            Data = GetStringFromReturnValue(returnValue);
            MethodName = methodName;
        }

        public static string GetStringFromReturnValue(object returnValue)
        {
            return JsonSerializer.Serialize(returnValue);
        }
    }
}