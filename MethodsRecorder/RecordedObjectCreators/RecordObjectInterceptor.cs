using Castle.DynamicProxy;
using MethodsRecorder.Writters;

namespace MethodsRecorder.RecordedObjectCreators
{
    internal class RecordObjectInterceptor : IInterceptor
    {
        private readonly IWritter Writter;

        public RecordObjectInterceptor(IWritter writter)
        {
            Writter = writter;
        }

        public void Intercept(IInvocation invocation)
        {
            Writter.Write(new MethodArgumentsData(invocation.Arguments, invocation.Method.Name));

            invocation.Proceed();

            Writter.Write(new MethodReturnValueData(invocation.ReturnValue, invocation.Method.Name));
            Writter.NextMethod();
        }
    }
}