using MyArchitecture.ApplicationLayer.Listener;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using System.Linq;

namespace MyListen.Player.Listeners
{

    public sealed class QueueChangedListener : Listener<IReadOnlyList<MusicInfos>>
    {
        public QueueChangedListener(PlaybackQueue queue, IMusicStore musicStore)
        {
            queue.PlaybackQueueChanged += (s, e) =>
            {
                var musics = musicStore.GetMusicsFromIds([.. e]);
                OnNotified([..musics.Select(MusicInfos.FromMusicEntity)]);
            };
        }
    }
}
