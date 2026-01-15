using MyListen.Common.DataTransfertObjects;
using MyListen.Song;

namespace MyListenApp.ViewModels.Song
{
    internal sealed class SongViewModelFactory(SongComposantFactory composantFactory)
    {
        readonly SongComposantFactory composantFactory = composantFactory;

        public SongViewModel CreateViewModel(MusicInfos infos)
        {
            return new SongViewModel(
                infos,
                composantFactory.CreateRenameMusic(),
                composantFactory.CreateChangeFavouriteState()
            );
        }
    }
}
