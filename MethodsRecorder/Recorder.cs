using System;

namespace MethodsRecorder
{
    public class Recorder
    {
        public RecordedObject<TInterface> CreateRecordedObject<TInterface>(TInterface instance)
            where TInterface : class
        {
            throw new NotImplementedException();
        }
    }
}