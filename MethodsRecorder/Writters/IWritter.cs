namespace MethodsRecorder.Writters
{
    public interface IWritter
    {
        Package Package { get; set; }
        void Write(MethodData data);
        void Init();
        void Complete();
    }
}
