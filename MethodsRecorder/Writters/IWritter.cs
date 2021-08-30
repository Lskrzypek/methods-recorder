namespace MethodsRecorder.Writters
{
    public interface IWritter
    {
        void Write(MethodData data);
        void WaitToCompleteWrite();
    }
}
