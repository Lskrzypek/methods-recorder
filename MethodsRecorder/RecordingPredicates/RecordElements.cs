using System;

namespace MethodsRecorder.RecordingPredicates
{
    [Flags]
    public enum RecordElements
    {
        Nothing = 0,
        ReturnValue = 1,
        Arguments = 2,
        All = ReturnValue | Arguments,
    }
}