using System;

namespace MethodsRecorder.Readers
{
    internal interface IReader
    {
        object ReadMethod(string methodName, object[] arguments, Type returnType);
    }
}