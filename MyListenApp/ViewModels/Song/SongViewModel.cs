using MyArchitecture;
using MyArchitecture.PresenterLayer;
using MyListen.Common.DataTransfertObjects;
using MyListen.Song;
using MyListenApp.Services;
using System;
using System.Windows.Input;

namespace MyListenApp.ViewModels.Song
{
    public sealed partial class SongViewModel : BaseViewModel
    {
        public Guid Id { get; }

        string _title;
        public string Title
        {
            get => _title;
            private set => SetValue(ref _title, value);
        }

        string _newTitle = string.Empty;
        public string NewTitle
        {
            get => _newTitle;
            set => SetValue(ref _newTitle, value);
        }

        string _artist;
        public string Artist
        {
            get => _artist;
            private set => SetValue(ref _artist, value);
        }

        TimeSpan _duration;
        public TimeSpan Duration
        {
            get => _duration;
            private set => SetValue(ref _duration, value);
        }

        bool _isLiked;
        public bool IsLiked
        {
            get => _isLiked;
            private set => SetValue(ref _isLiked, value);
        }

        public ICommand ToggleFavouriteCommand { get; }
        public ICommand RenameCommand { get; }


        readonly SongService songService;

        public SongViewModel(SongInfos infos, SongService service)
        {
            Id = infos.Id;
            _title = infos.Title;
            _artist = infos.Artist;
            _duration = infos.Duration;
            _isLiked = infos.IsLiked;
            this.songService = service;

            ToggleFavouriteCommand = new RelayCommand(execute: () =>
            {
                IsLiked = !IsLiked;
                songService.ChangeFavouriteState(Id, IsLiked);
            });

            RenameCommand = new RelayCommand(execute: () =>
            {
                Result result = songService.RenameSong(Id, NewTitle);
                if (result.IsSuccess) Title = NewTitle;
                NewTitle = string.Empty;
            });
        }

    }
}
