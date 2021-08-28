namespace MethodsRecorder.PlayingObjectCreators
{
    internal interface IPlayingObjectCreator
    {
        public PlayingObject<TInterface> Create<TInterface>()
            where TInterface : class;
    }
}