using System;
using MethodsRecorder.RecordedObjectCreators;
using MethodsRecorder.Writters;

namespace MethodsRecorder
{
    public class Recorder : IRecorder
    {
        public bool IsAsync { get; private set; }

        private readonly IWritter Writter;
        private readonly IRecordedObjectCreator RecordedObjectCreator;
        private readonly IWriteManager WriteManager;

        private bool isCompleted = false;

        public Recorder(IWritter writter, bool isAsync = false)
        {
            Writter = writter;
            IsAsync = isAsync;

            WriteManager = new WriteManagerFactory(Writter).Create(isAsync);
            RecordedObjectCreator = new RecordedObjectCreator(WriteManager);
        }

        public RecordedObject<TInterface> CreateRecordedObject<TInterface>(TInterface instance)
            where TInterface : class
        {
            if (!typeof(TInterface).IsInterface)
            {
                throw new ArgumentException("The recorded type must be an interface.");
            }

            if (isCompleted)
            {
                throw new Exception("The recording is completed.");
            }

            return RecordedObjectCreator.Create(instance);
        }

        public void CompleteWrite()
        {
            WriteManager.CompleteWrite();
            isCompleted = true;
        }
    }
}