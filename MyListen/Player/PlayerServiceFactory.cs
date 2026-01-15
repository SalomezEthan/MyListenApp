using MyListen.Common.Services;
using MyListen.Common.Services.Stores;

namespace MyListen.Player
{
    public sealed class PlayerServiceFactory(PlaybackQueue queue, ISongPlayer songPlayer, ISongRespository songRepo)
    {
        readonly PlaybackQueue queue = queue;
        readonly ISongPlayer songPlayer = songPlayer;
        readonly ISongRespository songRepo = songRepo;

        public PlayerService CreatePlayerService()
        {
            return new PlayerService(songPlayer, songRepo, queue);
        }
    }
}
