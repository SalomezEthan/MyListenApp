using MyArchitecture.ApplicationLayer.Listener;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;

namespace MyListen.Player.Listeners
{
    public sealed class SongChangedListener : Listener<SongInfos>
    {
        public SongChangedListener(PlaybackQueue queue, ISongStore songStore)
        {
            queue.CurrentSongIdChanged += (s, e) =>
            {
                Common.Entities.Song song = songStore.GetBySongId(e);
                var infos = SongInfos.FromSongEntity(song);
                OnNotified(infos);
            };
        }
    }
}
