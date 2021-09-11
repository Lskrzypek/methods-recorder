namespace MethodsRecorder.Writters
{
    internal interface IWriteManager
    {
        int CurrentRecordNumber { get; }
        bool Enabled { get; set; }

        void InitWrite();
        void CompleteWrite();
        void Write(MethodData data);
    }
}