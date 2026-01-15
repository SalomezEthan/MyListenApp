using MyArchitecture;
using MyArchitecture.ApplicationLayer;
using MyListen.Common.Services;

namespace MyListen.Player.UseCases
{
    public sealed record ChangePlaybackStateRequest(bool IsPlaying);
    public sealed class ChangePlaybackState(ISongPlayer songPlayer)
    : NoResponseUseCase<ChangePlaybackStateRequest>
    {
        readonly ISongPlayer songPlayer = songPlayer;

        public override void Execute(ChangePlaybackStateRequest request)
        {
            if (!request.IsPlaying) songPlayer.Play();
            else songPlayer.Pause();
        }
    }
}
