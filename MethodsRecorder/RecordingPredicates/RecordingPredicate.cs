using System;
using System.Reflection;

namespace MethodsRecorder.RecordingPredicates
{
    internal class RecordingPredicate : IRecordingPredicate
    {
        public RecordElements RecordElements { get; init; }
        private readonly Func<RecordingCase, bool> Predicate;       

        public RecordingPredicate(Func<RecordingCase, bool> predicate, RecordElements recordElements)
        {
            Predicate = predicate;
            RecordElements = recordElements;
        }

        public bool Check(MethodInfo methodInfo)
        {
            return Predicate(new RecordingCase(methodInfo));
        }
    }
}