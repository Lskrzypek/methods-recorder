using System.Linq;

namespace MethodsRecorder
{
    internal class MethodData
    {
        public int OrderNumber { get; set; }
        public string MethodName { get; set; }
        public object ReturnValue { get; set; }
        public object[] Arguments { get; set; }

        public bool AreArgumentsEquals(object[] args)
        {
            if (args.Length != Arguments.Length)
                return false;

            return Arguments.SequenceEqual(args);
        }
    }
}