using System;

namespace MethodsRecorder
{
    public class RecordedMethodParameters
    {
        public RecordedMethodParameters OnlyReturnValue()
        {
            throw new NotImplementedException();
        }

        public RecordedMethodParameters OnlyArguments()
        {
            throw new NotImplementedException();
        }

        public RecordedMethodParameters OnlyArguments(Func<MethodArgument, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}