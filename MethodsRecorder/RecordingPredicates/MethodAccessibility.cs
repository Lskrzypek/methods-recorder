using System;

namespace MethodsRecorder.RecordingPredicates
{
    [Flags]
    public enum MethodAccessibility
    {
        None = 0,
        Public = 1,
        Private = 2,
        All = Public | Private
    }
}