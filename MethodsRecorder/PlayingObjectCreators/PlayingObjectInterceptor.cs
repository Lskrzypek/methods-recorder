using Castle.DynamicProxy;
using MethodsRecorder.Readers;

namespace MethodsRecorder.PlayingObjectCreators
{
    internal class PlayingObjectInterceptor : IInterceptor
    {
        private readonly IReader Reader;

        public PlayingObjectInterceptor(IReader reader)
        {
            Reader = reader;
        }

        public void Intercept(IInvocation invocation)
        {
            var returnValue = Reader.ReadMethod(new ReaderInputData()
            {
                Arguments = invocation.Arguments,
                ClassName = invocation.Method.DeclaringType.FullName,
                MethodName = invocation.Method.Name,
                ReturnType = invocation.Method.ReturnType
            });
            invocation.ReturnValue = returnValue;
        }
    }
}