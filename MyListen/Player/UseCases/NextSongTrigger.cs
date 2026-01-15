using MyArchitecture;
using MyArchitecture.ApplicationLayer.UseCases.Sync;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.Player.UseCases
{
    public sealed class NextSongTrigger(PlaybackQueue queue, ISongPlayer songPlayer, ISongRespository songStore)
    : TriggerUseCase<Result>
    {
        readonly PlaybackQueue queue = queue;
        readonly ISongPlayer songPlayer = songPlayer;
        readonly ISongRespository songStore = songStore;

        public override void Execute()
        {
            Result<Guid> nextSongId = queue.NextSong();
            if (!nextSongId.IsSuccess)
            {
                Send(Result.Fail($"Erreur lors du passage au son suivant : {nextSongId.GetFailure().BrokenRule}"));
                return;
            }

            Reference songReference = songStore.GetSongReferenceById(nextSongId.GetValue());
            songPlayer.PlaySong(songReference);
            Send(Result.Ok());
        }
    }
}
