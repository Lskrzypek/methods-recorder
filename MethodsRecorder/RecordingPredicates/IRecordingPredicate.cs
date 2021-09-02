using System.Reflection;

namespace MethodsRecorder.RecordingPredicates
{
    internal interface IRecordingPredicate
    {
        bool Check(MethodInformations methodInformations);
        RecordElements RecordElements { get; }
    }
}