using System;

namespace MethodsRecorder.Readers
{
    internal interface IReader
    {
        object ReadMethod(string className, string methodName, object[] arguments, Type returnType);
    }
}