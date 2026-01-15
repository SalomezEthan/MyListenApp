using MyArchitecture;
using MyArchitecture.ApplicationLayer.UseCases.Sync;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.Player.UseCases
{
    public sealed class NextMusicTrigger(PlaybackQueue queue, IMusicPlayer musicPlayer, IMusicStore musicStore)
    : TriggerUseCase<Result>
    {
        readonly PlaybackQueue queue = queue;
        readonly IMusicPlayer musicPlayer = musicPlayer;
        readonly IMusicStore musicStore = musicStore;

        public override void Execute()
        {
            Result<Guid> nextSongId = queue.NextSong();
            if (!nextSongId.IsSuccess)
            {
                Send(Result.Fail($"Erreur lors du passage au son suivant : {nextSongId.GetFailure().BrokenRule}"));
                return;
            }

            Reference musicReference = musicStore.GetReferenceById(nextSongId.GetValue());
            musicPlayer.PlayMusic(musicReference);
            Send(Result.Ok());
        }
    }
}
