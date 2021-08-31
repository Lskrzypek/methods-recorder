namespace MethodsRecorder.Readers
{
    public interface IReader
    {
        object ReadMethod(ReaderInputData inputData);
    }
}