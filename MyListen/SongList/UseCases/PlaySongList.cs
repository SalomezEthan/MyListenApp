using MyArchitecture;
using MyArchitecture.ApplicationLayer;
using MyListen.Common.Entities;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.SongList.UseCases
{
    public sealed record PlaySongListRequest(Guid SongListId, Guid? StartSong = null);

    public sealed class PlaySongList(ISongListRepository songListRepo, PlaybackQueue playbackQueue, ISongRespository songRepo, ISongPlayer songPlayer)
    : UseCase<PlaySongListRequest>
    {
        readonly ISongListRepository songListRepo = songListRepo;
        readonly PlaybackQueue playbackQueue = playbackQueue;
        readonly ISongRespository songRepo = songRepo;
        readonly ISongPlayer songPlayer = songPlayer;

        public override void Execute(PlaySongListRequest request)
        {
            Common.Entities.SongList songList = songListRepo.GetSongListById(request.SongListId);
            Result<EnqueueList> enqueueList = EnqueueList.FromSongs(songList.SongIds);
            if (!enqueueList.IsSuccess)
            {
                Result.Fail($"Impossible de créer la liste de lecture : {enqueueList.GetFailure()}");
                return;
            }

            var enqueueListValue = enqueueList.GetValue();
            playbackQueue.SetPlaybackQueue(enqueueListValue);

            Guid startSongId = request.StartSong ?? enqueueListValue.Songs[0];
            playbackQueue.MoveTo(startSongId);

            Reference songReference = songRepo.GetSongReferenceById(startSongId);
            songPlayer.PlaySong(songReference);

            Send(Result.Ok());
        }
    }
}
