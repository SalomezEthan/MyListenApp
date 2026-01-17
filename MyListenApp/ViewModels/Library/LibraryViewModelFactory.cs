using MyListen.Library;
using MyListenApp.ViewModels.SongList;

namespace MyListenApp.ViewModels.Library
{
    internal sealed class LibraryViewModelFactory(LibraryServiceFactory composantFactory, SongListViewModelFactory factory)
    {
        readonly LibraryServiceFactory composantFactory = composantFactory;
        readonly SongListViewModelFactory factory = factory;

        public LibraryViewModel CreateLibraryViewModel()
        {
            return new LibraryViewModel(
                composantFactory.CreateLibraryService(),
                factory
            );
        }
    }
}
