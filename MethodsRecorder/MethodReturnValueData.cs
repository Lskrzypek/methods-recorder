using System.Text.Json;

namespace MethodsRecorder
{
    public class MethodReturnValueData : IDataToWrite
    {
        public string Data { get; private set; }
        public string Header => "#ReturnValue";

        public MethodReturnValueData(object returnValue)
        {
            Data = GetStringFromReturnValue(returnValue);
        }

        public static string GetStringFromReturnValue(object returnValue)
        {
            return JsonSerializer.Serialize(returnValue);
        }
    }
}