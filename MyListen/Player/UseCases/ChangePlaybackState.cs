using MyArchitecture;
using MyArchitecture.ApplicationLayer;
using MyListen.Common.Services;

namespace MyListen.Player.UseCases
{
    public sealed record ChangePlaybackStateRequest(bool IsPlaying);
    public sealed class ChangePlaybackState(IMusicPlayer musicPlayer)
    : NoResponseUseCase<ChangePlaybackStateRequest>
    {
        public override void Execute(ChangePlaybackStateRequest request)
        {
            if (!request.IsPlaying) musicPlayer.Play();
            else musicPlayer.Pause();
        }
    }
}
