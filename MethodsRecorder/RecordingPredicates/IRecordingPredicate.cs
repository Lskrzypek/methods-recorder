using System.Reflection;

namespace MethodsRecorder.RecordingPredicates
{
    internal interface IRecordingPredicate
    {
        bool Check(MethodInfo methodInfo);
        RecordElements RecordElements { get; }
    }
}