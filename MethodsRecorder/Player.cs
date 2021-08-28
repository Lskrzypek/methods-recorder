using System;
using MethodsRecorder.PlayingObjectCreators;
using MethodsRecorder.Readers;

namespace MethodsRecorder
{
    public class Player
    {
        private readonly IPlayingObjectCreator PlayingObjectCreator;
        private readonly IReader Reader;

        public Player(string filePath)
        {
            Reader = new FileReader(filePath);
            PlayingObjectCreator = new PlayingObjectCreator(Reader);
        }

        public PlayingObject<TInterface> CreatePlayingObject<TInterface>()
            where TInterface : class
        {
            if (!typeof(TInterface).IsInterface)
            {
                throw new ArgumentException("The playing type must be an interface.");
            }

            return PlayingObjectCreator.Create<TInterface>();
        }
    }
}