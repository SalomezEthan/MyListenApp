using MyListen.Player;
using MyListenApp.ViewModels.Song;

namespace MyListenApp.ViewModels.Player
{
    internal sealed class PlayerViewModelFactory (PlayerComposantFactory composantFactory, SongViewModelMap songViewModelMap)
    {
        readonly PlayerComposantFactory factory = composantFactory;
        readonly SongViewModelMap songViewModelMap = songViewModelMap;

        public PlayerViewModel CreatePlayerViewModel()
        {
            return new PlayerViewModel(
                factory.CreateChangePlaybackState(),
                factory.CreateNextMusicTrigger(),
                factory.CreatePreviousMusicTrigger(),
                factory.CreateChangeVolume(),
                factory.CreateMusicChangedListener(),
                factory.CreateQueueChangedListener(),
                factory.CreateStateChangedListener(),
                factory.CreateSongEndedListener(),
                songViewModelMap
            );
        }
    }
}
