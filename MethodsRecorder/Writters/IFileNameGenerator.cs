namespace MethodsRecorder.Writters
{
    public interface IFileNameGenerator
    {
        string GenerateFileName(MethodData data, Package package);
    }
}
