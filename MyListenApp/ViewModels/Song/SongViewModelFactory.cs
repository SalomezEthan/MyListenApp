using MyListen.Common.DataTransfertObjects;
using MyListen.Song;

namespace MyListenApp.ViewModels.Song
{
    internal sealed class SongViewModelFactory(SongComposantFactory composantFactory)
    {
        readonly SongComposantFactory composantFactory = composantFactory;

        public SongViewModel CreateViewModel(SongInfos infos)
        {
            return new SongViewModel(
                infos,
                composantFactory.CreateRenameSong(),
                composantFactory.CreateChangeFavouriteState()
            );
        }
    }
}
