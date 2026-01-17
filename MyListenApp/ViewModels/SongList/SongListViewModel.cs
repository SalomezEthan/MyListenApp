using MyArchitecture;
using MyArchitecture.PresenterLayer;
using MyListen.Common.DataTransfertObjects;
using MyListen.SongList;
using MyListenApp.Services;
using MyListenApp.ViewModels.Song;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MyListenApp.ViewModels.SongList
{
    internal sealed partial class SongListViewModel : BaseViewModel
    {
        public Guid Id { get; }

        string _name;
        public string Name
        {
            get => _name;
            set => SetValue(ref _name, value);
        }

        int _count;
        public int Count
        {
            get => _count;
            set => SetValue(ref _count, value);
        }

        ObservableCollection<SongViewModel> _songs = [];
        public ObservableCollection<SongViewModel> Songs
        {
            get => _songs;
            set => SetValue(ref _songs, value);
        }

        public ICommand RefreshSongsCommand { get; }
        public ICommand PlayPlaylistCommand { get; }

        readonly SongListService songListService;

        internal SongListViewModel(SongListInfos infos, SongListService service, SongViewModelMap songViewModelMap)
        {
            this.Id = infos.Id;
            this._name = infos.Name;
            this._count = infos.Count;

            this.songListService = service;

            RefreshSongsCommand = new RelayCommand(() =>
            {
                var songsInfos = songListService.CollectSongs(Id);
                var songViewModels = from songInfos in songsInfos
                                     select songViewModelMap.GetSafeWithSongInfos(songInfos);
                Songs = new ObservableCollection<SongViewModel>(songViewModels);
                Count = Songs.Count;
            });

            PlayPlaylistCommand = new RelayCommand(() =>
            {
                Result result = songListService.PlaySongList(Id);
            });
        }
    }
}
