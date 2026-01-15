using MyArchitecture;
using MyArchitecture.ApplicationLayer.UseCases.Sync;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.Player.UseCases
{
    public sealed class PreviousMusicTrigger(PlaybackQueue queue, IMusicPlayer musicPlayer, IMusicStore musicStore)
    : TriggerUseCase<Result>
    {
        readonly PlaybackQueue queue = queue;
        readonly IMusicPlayer musicPlayer = musicPlayer;
        readonly IMusicStore musicStore = musicStore;

        public override void Execute()
        {
            Result<Guid> previousSongId = queue.PreviousSong();
            if (!previousSongId.IsSuccess)
            {
                Send(Result.Fail($"Erreur lors du passage au song précédent : {previousSongId.GetFailure()}"));
                return;
            }
            
            Reference reference = musicStore.GetReferenceById(previousSongId.GetValue());
            musicPlayer.PlayMusic(reference);
            Send(Result.Ok());
        }
    }
}
