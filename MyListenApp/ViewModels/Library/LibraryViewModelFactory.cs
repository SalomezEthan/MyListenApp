using MyListen.Library;
using MyListenApp.ViewModels.SongList;

namespace MyListenApp.ViewModels.Library
{
    internal sealed class LibraryViewModelFactory(LibraryComposantFactory composantFactory, SongListViewModelFactory factory)
    {
        readonly LibraryComposantFactory composantFactory = composantFactory;
        readonly SongListViewModelFactory factory = factory;

        public LibraryViewModel CreateLibraryViewModel()
        {
            return new LibraryViewModel(
                composantFactory.CreateCollectPlaylist(),
                composantFactory.CreateImportPlaylist(),
                factory
            );
        }
    }
}
