namespace MethodsRecorder
{
    public class MethodData
    {
        public int OrderNumber { get; set; }
        public string ClasName { get; set; }
        public string MethodName { get; set; }
        public object ReturnValue { get; set; }
        public object[] Arguments { get; set; }
    }
}