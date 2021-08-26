namespace MethodsRecorder
{
    internal interface IDataToWrite
    {
        string Data { get; }
        string Header { get; }
        string MethodName { get; }
    }
}