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

        readonly CollectPlaylistTrigger playlistCollector;
        readonly ImportPlaylist playlistImporter;
        public LibraryViewModel(CollectPlaylistTrigger playlistCollector, ImportPlaylist playlistImporter, SongListViewModelFactory songListFactory)
        {
            this.playlistCollector = playlistCollector;
            this.playlistCollector.ResultSended += (s, e) =>
            {
                SongLists = [.. e.Playlists.Select(songListFactory.CreateSongList)];
            };

            this.playlistImporter = playlistImporter;
            this.playlistImporter.ResultSended += (s, e) =>
            {
                if (e.IsSuccess)
                {
                    var newSongListViewModel = songListFactory.CreateSongList(e.GetValue());
                    SongLists.Add(newSongListViewModel);
                }
            };
        }

        public void CollectPlaylist() => playlistCollector.Execute();

        public void ImportPlaylist(string reference)
        {
            var request = new ImportPlaylistRequest(reference);
            playlistImporter.Execute(request);
        }
    }
}
