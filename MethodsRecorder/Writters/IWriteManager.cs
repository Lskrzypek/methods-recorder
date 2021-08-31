namespace MethodsRecorder.Writters
{
    public interface IWriteManager
    {
        int CurrentRecordNumber { get; }

        void CompleteWrite();
        void Write(MethodData data);
    }
}