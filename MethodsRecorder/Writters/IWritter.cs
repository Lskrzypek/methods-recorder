namespace MethodsRecorder.Writters
{
    internal interface IWritter
    {
        void Write(IDataToWrite data);
        void NextMethod();
    }
}
