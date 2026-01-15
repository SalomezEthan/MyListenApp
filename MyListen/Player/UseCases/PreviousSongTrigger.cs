using MyArchitecture;
using MyArchitecture.ApplicationLayer.UseCases.Sync;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.Player.UseCases
{
    public sealed class PreviousSongTrigger(PlaybackQueue queue, ISongPlayer songPlayer, ISongRespository songRepo)
    : TriggerUseCase<Result>
    {
        readonly PlaybackQueue queue = queue;
        readonly ISongPlayer songPlayer = songPlayer;
        readonly ISongRespository songRepo = songRepo;

        public override void Execute()
        {
            Result<Guid> previousSongId = queue.PreviousSong();
            if (!previousSongId.IsSuccess)
            {
                Send(Result.Fail($"Erreur lors du passage au song précédent : {previousSongId.GetFailure()}"));
                return;
            }
            
            Reference reference = songRepo.GetSongReferenceById(previousSongId.GetValue());
            songPlayer.PlaySong(reference);
            Send(Result.Ok());
        }
    }
}
