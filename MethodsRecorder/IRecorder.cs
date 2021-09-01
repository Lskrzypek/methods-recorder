using System;

namespace MethodsRecorder
{
    public interface IRecorder : IDisposable
    {
        bool IsAsync { get; }

        RecordedObject<TInterface> CreateRecordedObject<TInterface>(TInterface instance) where TInterface : class;
    }
}