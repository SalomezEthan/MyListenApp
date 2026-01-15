using MyArchitecture;
using MyArchitecture.ApplicationLayer;
using MyListen.Common.Entities;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.SongList.UseCases
{
    public sealed record PlaySongListRequest(Guid PlaylistId, Guid? StartSong = null);

    public sealed class PlaySongList(IPlaylistStore playlistStore, PlaybackQueue playbackQueue, ISongStore songStore, ISongPlayer songPlayer)
    : UseCase<PlaySongListRequest>
    {
        readonly IPlaylistStore playlistStore = playlistStore;
        readonly PlaybackQueue playbackQueue = playbackQueue;
        readonly ISongStore songStore = songStore;
        readonly ISongPlayer songPlayer = songPlayer;

        public override void Execute(PlaySongListRequest request)
        {
            Playlist playlist = playlistStore.GetFromPlaylistId(request.PlaylistId);
            Result<EnqueueList> enqueueList = EnqueueList.FromSongs(playlist.SongIds);
            if (!enqueueList.IsSuccess)
            {
                Result.Fail($"Impossible de créer la liste de lecture : {enqueueList.GetFailure()}");
                return;
            }

            var songList = enqueueList.GetValue();
            playbackQueue.SetPlaybackQueue(songList);

            Guid startSongId = request.StartSong ?? songList.Songs[0];
            playbackQueue.MoveTo(startSongId);

            Reference songReference = songStore.GetReferenceById(startSongId);
            songPlayer.PlaySong(songReference);

            Send(Result.Ok());
        }
    }
}
