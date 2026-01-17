using MyArchitecture.PresenterLayer;
using MyListen.Library;
using MyListenApp.Services;
using MyListenApp.ViewModels.SongList;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MyListenApp.ViewModels.Library
{
    internal sealed partial class LibraryViewModel
    : BaseViewModel
    {
        ObservableCollection<SongListViewModel> _songLists = [];
        public ObservableCollection<SongListViewModel> SongLists
        {
            get => _songLists;
            set => SetValue(ref _songLists, value);
        }

        readonly LibraryService libraryService;
        readonly SongListViewModelFactory songListViewModelFactory;
        public LibraryViewModel(LibraryService service, SongListViewModelFactory songListFactory)
        {
            this.libraryService = service;
        }

        public void CollectSongLists()
        {
            var songLists = libraryService.CollectSongs();
            SongLists = [.. songLists.Select(songListViewModelFactory.CreateSongList)];
        }

        public void ImportSongList(string reference)
        {
            var importedSongList = libraryService.ImportSongList(reference);
            if (importedSongList.IsSuccess)
            {
                SongLists.Add(songListViewModelFactory.CreateSongList(importedSongList.GetValue()));
            }
        }
    }
}
