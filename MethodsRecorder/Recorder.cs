using System;
using MethodsRecorder.Writters;
using MethodsRecorder.RecordedObjectCreators;

namespace MethodsRecorder
{
    public class Recorder
    {
        private readonly IWritter Writter;
        private readonly IRecordedObjectCreator RecordedObjectCreator;

        public Recorder(string filePath) 
        {
            Writter = new FileWritter(filePath);
            RecordedObjectCreator = new RecordedObjectCreator(Writter);
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
    }
}