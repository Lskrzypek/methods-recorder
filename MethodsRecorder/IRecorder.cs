namespace MethodsRecorder
{
    public interface IRecorder
    {
        bool IsAsync { get; }

        void CompleteWrite();
        RecordedObject<TInterface> CreateRecordedObject<TInterface>(TInterface instance) where TInterface : class;
    }
}