using MyListen.Common.Entities;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.SongList.UseCases;

namespace MyListen.SongList
{
    public sealed class SongListComposantFactory(IPlaylistStore playlistStore, IMusicStore musicStore, PlaybackQueue playbackQueue, IMusicPlayer musicPlayer)
    {
        readonly IPlaylistStore playlistStore = playlistStore;
        readonly IMusicStore musicStore = musicStore;
        readonly PlaybackQueue playbackQueue = playbackQueue;

        public CollectMusics CreateCollectMusics()
        {
            return new CollectMusics(musicStore, playlistStore);
        }

        public PlaySongList CreatePlaySongList()
        {
            return new PlaySongList(playlistStore, playbackQueue, musicStore, musicPlayer);
        }

        public RenamePlaylist CreateRenamePlaylist()
        {
            return new RenamePlaylist(playlistStore);
        }
    }
}
