using MethodsRecorder.RecordedObjectCreators;
using MethodsRecorder.RecordingPredicates;
using System;

namespace MethodsRecorder
{
    public class RecordedObject<T>
    {
        public T Object { get; }
        public T OrginalObject { get; }

        private readonly IRecordObjectInterceptor Interceptor;

        internal RecordedObject(T obj, T orginalObject, IRecordObjectInterceptor interceptor)
        {
            Object = obj;
            OrginalObject = orginalObject;
            Interceptor = interceptor;
        }

        public RecordedObject<T> Record(Func<RecordingCase, bool> predicate, RecordElements recordElements = RecordElements.All)
        {
            Interceptor.Constraints.Add(new RecordingPredicate(predicate, recordElements));
            return this;
        }
    }
}