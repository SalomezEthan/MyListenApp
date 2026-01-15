using MyListen.Common.Entities;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.SongList.UseCases;

namespace MyListen.SongList
{
    public sealed class SongListComposantFactory(IPlaylistRepository playlistStore, ISongRespository songStore, PlaybackQueue playbackQueue, ISongPlayer songPlayer)
    {
        readonly IPlaylistRepository playlistRepo = playlistStore;
        readonly ISongRespository songRepo = songStore;
        readonly PlaybackQueue playbackQueue = playbackQueue;

        public CollectSongs CreateCollectSongs()
        {
            return new CollectSongs(songRepo, playlistRepo);
        }

        public PlaySongList CreatePlaySongList()
        {
            return new PlaySongList(playlistRepo, playbackQueue, songRepo, songPlayer);
        }

        public RenamePlaylist CreateRenamePlaylist()
        {
            return new RenamePlaylist(playlistRepo);
        }
    }
}
