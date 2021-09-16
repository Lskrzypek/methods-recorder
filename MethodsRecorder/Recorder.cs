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
        private bool _disposed = false;

        public Recorder(string directoryPath, bool isAsync = false)
        {
            Writter = new FileWritter(directoryPath);
            IsAsync = isAsync;

            WriteManager = new WriteManagerFactory(Writter).Create(isAsync);
            RecordedObjectCreator = new RecordedObjectCreator(WriteManager);
        }

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

            return RecordedObjectCreator.Create(instance);
        }

        public void StartRecording()
        {
            WriteManager.InitWrite();
        }

        public void StopRecording()
        {
            WriteManager.CompleteWrite();
        }

        public void Pause()
        {
            WriteManager.Enabled = false;
        }

        public void Unpause()
        {
            WriteManager.Enabled = true;
        }

        #region Dispose
        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                StopRecording();
            }

            _disposed = true;
        }
        #endregion
    }
}