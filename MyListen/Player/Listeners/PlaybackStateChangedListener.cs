using MyArchitecture.ApplicationLayer.Listener;
using MyListen.Common.Services;

namespace MyListen.Player.Listeners
{

    public sealed record NewPlaybackState(bool IsPlaying);
    public class PlaybackStateChangedListener : Listener<NewPlaybackState>
    {
        public PlaybackStateChangedListener(IMusicPlayer player)
        {
            player.StateChanged += (s, e) =>
            {
                OnNotified(new NewPlaybackState(e.IsPlaying));
            };
        }
    }
}
