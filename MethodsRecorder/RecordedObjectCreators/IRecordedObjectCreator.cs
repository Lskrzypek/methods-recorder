namespace MethodsRecorder.RecordedObjectCreators
{
    internal interface IRecordedObjectCreator
    {
        RecordedObject<TInterface> Create<TInterface>(TInterface instance)
            where TInterface : class;
    }
}
