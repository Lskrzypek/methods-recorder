﻿namespace MethodsRecorder.Writters
{
    internal class WriteManager : IWriteManager
    {
        public int CurrentRecordNumber { get; private set; }

        private readonly IWritter Writter;

        public WriteManager(IWritter writter)
        {
            Writter = writter;
        }

        public void Write(MethodData data)
        {
            CurrentRecordNumber++;
            Writter.Write(data);
        }

        public void CompleteWrite()
        { }
    }
}