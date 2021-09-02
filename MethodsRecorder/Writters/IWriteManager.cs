namespace MethodsRecorder.Writters
{
    internal interface IWriteManager
    {
        int CurrentRecordNumber { get; }

        void CompleteWrite();
        void Write(MethodData data);
    }
}