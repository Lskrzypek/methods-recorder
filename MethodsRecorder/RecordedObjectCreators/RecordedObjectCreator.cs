using Castle.DynamicProxy;
using MethodsRecorder.Writters;

namespace MethodsRecorder.RecordedObjectCreators
{
    internal class RecordedObjectCreator : IRecordedObjectCreator
    {
        private readonly IWritter Writter;
        private readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

        public RecordedObjectCreator(IWritter writter)
        {
            Writter = writter;
        }

        public RecordedObject<TInterface> Create<TInterface>(TInterface instance)
            where TInterface : class
        {
            var proxyObject = ProxyGenerator.CreateInterfaceProxyWithTarget(instance, new RecordObjectInterceptor(Writter));

            return new RecordedObject<TInterface>(obj: proxyObject, orginalObject: instance);
        }
    }
}