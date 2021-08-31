namespace MethodsRecorder.Writters
{
    public class WriteManager : IWriteManager
    {
        public int CurrentRecordNumber { get; private set; }

        private readonly IWritter Writter;

        public WriteManager(IWritter writter)
        {
            Writter = writter;
        }

        public void Write(MethodData data)
        {
            Writter.Write(data);
            CurrentRecordNumber++;
        }

        public void CompleteWrite()
        { }
    }
}