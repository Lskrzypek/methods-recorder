using Castle.DynamicProxy;
using MethodsRecorder.Writters;

namespace MethodsRecorder.RecordedObjectCreators
{
    internal class RecordObjectInterceptor : IInterceptor
    {
        private readonly IWriteManager WriteManager;

        public RecordObjectInterceptor(IWriteManager writeManager)
        {
            WriteManager = writeManager;
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            var data = new MethodData()
            {
                Arguments = invocation.Arguments,
                ReturnValue = invocation.ReturnValue,
                MethodName = invocation.Method.Name,
                ClasName = invocation.Method.DeclaringType.FullName,
                OrderNumber = WriteManager.CurrentRecordNumber
            };

            WriteManager.Write(data);
        }
    }
}