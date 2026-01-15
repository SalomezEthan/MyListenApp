using MyArchitecture;
using MyArchitecture.ApplicationLayer;
using MyListen.Common.Entities;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.SongList.UseCases
{
    public sealed record PlaySongListRequest(Guid PlaylistId, Guid? StartMusic = null);

    public sealed class PlaySongList(IPlaylistStore playlistStore, PlaybackQueue playbackQueue, IMusicStore musicStore, IMusicPlayer musicPlayer)
    : UseCase<PlaySongListRequest>
    {
        readonly IPlaylistStore playlistStore = playlistStore;
        readonly PlaybackQueue playbackQueue = playbackQueue;
        readonly IMusicStore musicStore = musicStore;
        readonly IMusicPlayer musicPlayer = musicPlayer;

        public override void Execute(PlaySongListRequest request)
        {
            Playlist playlist = playlistStore.GetFromPlaylistId(request.PlaylistId);
            Result<EnqueueList> enqueueList = EnqueueList.FromSongs(playlist.MusicIds);
            if (!enqueueList.IsSuccess)
            {
                Result.Fail($"Impossible de créer la liste de lecture : {enqueueList.GetFailure()}");
                return;
            }

            var songList = enqueueList.GetValue();
            playbackQueue.SetPlaybackQueue(songList);

            Guid startMusicId = request.StartMusic ?? songList.Songs[0];
            playbackQueue.MoveTo(startMusicId);

            Reference musicReference = musicStore.GetReferenceById(startMusicId);
            musicPlayer.PlayMusic(musicReference);

            Send(Result.Ok());
        }
    }
}
