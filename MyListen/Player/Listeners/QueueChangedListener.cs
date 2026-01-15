using MyArchitecture.ApplicationLayer.Listener;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using System.Linq;

namespace MyListen.Player.Listeners
{

    public sealed class QueueChangedListener : Listener<IReadOnlyList<SongInfos>>
    {
        public QueueChangedListener(PlaybackQueue queue, ISongRespository songStore)
        {
            queue.PlaybackQueueChanged += (s, e) =>
            {
                var songs = songStore.GetSongsByIds([.. e]);
                OnNotified([..songs.Select(SongInfos.FromSongEntity)]);
            };
        }
    }
}
