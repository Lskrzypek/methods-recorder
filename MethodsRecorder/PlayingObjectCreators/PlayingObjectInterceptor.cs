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
            var returnValue = Reader.ReadMethod(
                invocation.Method.DeclaringType.FullName, 
                invocation.Method.Name, 
                invocation.Arguments, 
                invocation.Method.ReturnType);
            invocation.ReturnValue = returnValue;
        }
    }
}