using MyArchitecture.ApplicationLayer.Listener;
using MyListen.Common.Services;

namespace MyListen.Player.Listeners
{
    public sealed class SongEndedListener : Listener
    {
        public SongEndedListener(ISongPlayer songPlayer)
        {
            songPlayer.SongEnded += (s, e) =>
            {
                OnNotified();
            };
        }
    }
}
