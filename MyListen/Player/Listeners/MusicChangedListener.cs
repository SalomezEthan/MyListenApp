using MyArchitecture.ApplicationLayer.Listener;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;

namespace MyListen.Player.Listeners
{
    public sealed class MusicChangedListener : Listener<MusicInfos>
    {
        public MusicChangedListener(PlaybackQueue queue, IMusicStore musicStore)
        {
            queue.CurrentMusicIdChanged += (s, e) =>
            {
                Common.Entities.Song music = musicStore.GetByMusicId(e);
                var infos = MusicInfos.FromMusicEntity(music);
                OnNotified(infos);
            };
        }
    }
}
