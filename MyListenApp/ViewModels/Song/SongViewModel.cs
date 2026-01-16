using MyArchitecture.PresenterLayer;
using MyListen.Common.DataTransfertObjects;
using MyListen.Song;
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
            set => SetValue(ref _title, value);
        }

        string _artist;
        public string Artist
        {
            get => _artist;
            set => SetValue(ref _artist, value);
        }

        TimeSpan _duration;
        public TimeSpan Duration
        {
            get => _duration;
            set => SetValue(ref _duration, value);
        }

        bool _isLiked;
        public bool IsLiked
        {
            get => _isLiked;
            set => SetValue(ref _isLiked, value);
        }

        public ICommand ToggleFavouriteCommand { get; }
        public ICommand RenameCommand { get; }


        readonly SongService songService;

        public SongViewModel(SongInfos infos, SongService songService)
        {
            Id = infos.Id;
            _title = infos.Title;
            _artist = infos.Artist;
            _duration = infos.Duration;
            _isLiked = infos.IsLiked;

            this.songService = songService;
        }

    }
}
