using MyListen.Common.Entities;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.SongList.UseCases;

namespace MyListen.SongList
{
    public sealed class SongListComposantFactory(IPlaylistStore playlistStore, ISongStore songStore, PlaybackQueue playbackQueue, ISongPlayer songPlayer)
    {
        readonly IPlaylistStore playlistStore = playlistStore;
        readonly ISongStore songStore = songStore;
        readonly PlaybackQueue playbackQueue = playbackQueue;

        public CollectSongs CreateCollectSongs()
        {
            return new CollectSongs(songStore, playlistStore);
        }

        public PlaySongList CreatePlaySongList()
        {
            return new PlaySongList(playlistStore, playbackQueue, songStore, songPlayer);
        }

        public RenamePlaylist CreateRenamePlaylist()
        {
            return new RenamePlaylist(playlistStore);
        }
    }
}
