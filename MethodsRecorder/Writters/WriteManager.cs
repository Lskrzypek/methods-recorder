namespace MethodsRecorder.Writters
{
    internal class WriteManager : IWriteManager
    {
        public int CurrentRecordNumber { get; private set; }

        private readonly IWritter Writter;

        public bool Enabled { get; set; } = false;

        public WriteManager(IWritter writter)
        {
            Writter = writter;
        }

        public void Write(MethodData data)
        {
            if (!Enabled)
                return;

            CurrentRecordNumber++;
            Writter.Write(data);
        }

        public void CompleteWrite()
        {
            Enabled = false;
        }

        public void InitWrite()
        {
            if (Enabled)
                return;

            CurrentRecordNumber = 0;
            Enabled = true;

            Writter.Init();
        }
    }
}