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
            invocation.Proceed();

            var data = new MethodData()
            {
                Arguments = invocation.Arguments,
                ReturnValue = invocation.ReturnValue,
                MethodName = invocation.Method.Name
            };

            Writter.Write(data);
        }
    }
}