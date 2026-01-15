using MyListen.Player;
using MyListenApp.ViewModels.Song;

namespace MyListenApp.ViewModels.Player
{
    internal sealed class PlayerViewModelFactory (PlayerServiceFactory composantFactory, SongViewModelMap songViewModelMap)
    {
        readonly PlayerServiceFactory factory = composantFactory;
        readonly SongViewModelMap songViewModelMap = songViewModelMap;

        public PlayerViewModel CreatePlayerViewModel()
        {
            return new PlayerViewModel(
                factory.CreatePlayerService(),
                songViewModelMap
            );
        }
    }
}
