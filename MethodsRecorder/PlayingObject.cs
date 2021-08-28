namespace MethodsRecorder
{
    public class PlayingObject<T>
    {
        public T Object { get; }

        public PlayingObject(T obj)
        {
            Object = obj;
        }
    }
}