using MyListen.Common.Entities;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.SongList.UseCases;

namespace MyListen.SongList
{
    public sealed class SongListComposantFactory(ISongListRepository songListRepo, ISongRespository songRepo, PlaybackQueue playbackQueue, ISongPlayer songPlayer)
    {
        readonly ISongListRepository songListRepo = songListRepo;
        readonly ISongRespository songRepo = songRepo;
        readonly PlaybackQueue playbackQueue = playbackQueue;

        public CollectSongs CreateCollectSongs()
        {
            return new CollectSongs(songRepo, songListRepo);
        }

        public PlaySongList CreatePlaySongList()
        {
            return new PlaySongList(songListRepo, playbackQueue, songRepo, songPlayer);
        }

        public RenameSongList CreateRenameSongList()
        {
            return new RenameSongList(songListRepo);
        }
    }
}
