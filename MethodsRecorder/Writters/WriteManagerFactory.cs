namespace MethodsRecorder.Writters
{
    public class WriteManagerFactory
    {
        private readonly IWritter Writter;

        public WriteManagerFactory(IWritter writter)
        {
            Writter = writter;
        }

        public IWriteManager Create(bool isAsync)
        {
            if (isAsync)
                return new AsyncWriteManager(Writter);

            return new WriteManager(Writter);
        }
    }
}