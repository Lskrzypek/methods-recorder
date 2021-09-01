using MethodsRecorder.Readers;
using Castle.DynamicProxy;

namespace MethodsRecorder.PlayingObjectCreators
{
    internal class PlayingObjectCreator : IPlayingObjectCreator
    {
        private readonly ProxyGenerator ProxyGenerator = new();
        private readonly IReader Reader;

        public PlayingObjectCreator(IReader reader)
        {
            Reader = reader;
        }

        public PlayingObject<TInterface> Create<TInterface>() 
            where TInterface : class
        {
            var proxyObject = ProxyGenerator.CreateInterfaceProxyWithoutTarget<TInterface>(new PlayingObjectInterceptor(Reader));

            return new PlayingObject<TInterface>(proxyObject);
        }
    }
}