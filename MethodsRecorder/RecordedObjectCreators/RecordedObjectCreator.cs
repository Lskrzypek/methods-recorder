using Castle.DynamicProxy;
using MethodsRecorder.Writters;

namespace MethodsRecorder.RecordedObjectCreators
{
    internal class RecordedObjectCreator : IRecordedObjectCreator
    {
        private readonly IWriteManager WriteManager;
        private readonly ProxyGenerator ProxyGenerator = new();

        public RecordedObjectCreator(IWriteManager writeManager)
        {
            WriteManager = writeManager;
        }

        public RecordedObject<TInterface> Create<TInterface>(TInterface instance)
            where TInterface : class
        {
            IInterceptor interceptor = new RecordObjectInterceptor(WriteManager);
            var proxyObject = ProxyGenerator.CreateInterfaceProxyWithTarget(instance, interceptor);

            return new RecordedObject<TInterface>(
                obj: proxyObject, 
                orginalObject: instance,
                interceptor: interceptor as IRecordObjectInterceptor);
        }
    }
}