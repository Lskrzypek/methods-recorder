using System.Reflection;

namespace MethodsRecorder.RecordingPredicates
{
    public class MethodInformations
    {
        public string MethodName { get; set; }
        public MethodInfo ReflectionMethodInfo { get; init; }
        public ArgumentValue[] Arguments { get; init; }
    }
}