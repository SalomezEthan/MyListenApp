using MyListen.Common.Entities;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;

namespace MyListen.SongList
{
    public sealed class SongListComposantFactory(ISongListRepository songListRepo, ISongRespository songRepo, PlaybackQueue playbackQueue, ISongPlayer songPlayer)
    {
        readonly ISongListRepository songListRepo = songListRepo;
        readonly ISongRespository songRepo = songRepo;
        readonly PlaybackQueue playbackQueue = playbackQueue;
        readonly ISongPlayer songPlayer = songPlayer;

        public SongListService CreateSongListService()
        {
            return new SongListService(songRepo, songListRepo, playbackQueue, songPlayer);
        }
    }
}
