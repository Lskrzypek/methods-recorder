namespace MethodsRecorder
{
    public interface IPlayer
    {
        PlayingObject<TInterface> CreatePlayingObject<TInterface>() where TInterface : class;
    }
}