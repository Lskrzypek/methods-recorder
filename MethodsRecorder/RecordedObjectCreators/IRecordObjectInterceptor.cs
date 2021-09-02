using MethodsRecorder.RecordingPredicates;
using System.Collections.Generic;

namespace MethodsRecorder.RecordedObjectCreators
{
    internal interface IRecordObjectInterceptor
    {
        List<IRecordingPredicate> Constraints { get; set; }
    }
}