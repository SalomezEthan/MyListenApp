using MyArchitecture.PresenterLayer;
using MyListen.Library.UseCases;
using MyListenApp.ViewModels.SongList;
using System.Collections.ObjectModel;
using System.Linq;

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

        readonly CollectSongList songListRepo;
        readonly ImportSongList songListImporter;
        public LibraryViewModel(CollectSongList songListRepo, ImportSongList songListImporter, SongListViewModelFactory songListFactory)
        {
            this.songListRepo = songListRepo;
            this.songListRepo.ResultSended += (s, e) =>
            {
                SongLists = [.. e.SongLists.Select(songListFactory.CreateSongList)];
            };

            this.songListImporter = songListImporter;
            this.songListImporter.ResultSended += (s, e) =>
            {
                if (e.IsSuccess)
                {
                    var newSongListViewModel = songListFactory.CreateSongList(e.GetValue());
                    SongLists.Add(newSongListViewModel);
                }
            };
        }

        public void CollectSongList() => songListRepo.Execute();

        public void ImportSongList(string reference)
        {
            var request = new ImportSongListRequest(reference);
            songListImporter.Execute(request);
        }
    }
}
