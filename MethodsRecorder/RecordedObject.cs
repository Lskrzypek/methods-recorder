using System;

namespace MethodsRecorder
{
    public class RecordedObject<T>
    {
        public T Object { get; }
        public T OrginalObject { get; }

        public RecordedObject(T obj, T orginalObject)
        {
            Object = obj;
            OrginalObject = orginalObject;
        }

        public RecordedObject<T> RecordWhen(Func<Method, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public RecordedObject<T> RecordAlways()
        {
            throw new NotImplementedException();
        }

        public RecordedObject<T> SetupMethod(string methodName, Action<RecordedMethodParameters> parameters)
        {
            throw new NotImplementedException();
        }
    }
}