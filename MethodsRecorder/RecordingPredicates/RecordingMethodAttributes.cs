using System;

namespace MethodsRecorder.RecordingPredicates
{
    public struct RecordingMethodAttributes
    {
        public MethodAccessibility Accessibility { get;  set; }
        public Type ReturnType { get;  set; }
    }
}