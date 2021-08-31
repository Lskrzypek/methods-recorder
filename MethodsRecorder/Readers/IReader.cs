namespace MethodsRecorder.Readers
{
    internal interface IReader
    {
        object ReadMethod(ReaderInputData inputData);
    }
}