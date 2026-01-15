using MyArchitecture.PresenterLayer;
using MyListen.Common.DataTransfertObjects;
using MyListen.Song.UseCases;
using System;

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

        readonly RenameSong renameSong;
        readonly ChangeFavouriteState changeFavouriteState;

        public SongViewModel(SongInfos infos, RenameSong renameSong, ChangeFavouriteState changeFavouriteState)
        {
            Id = infos.Id;
            _title = infos.Title;
            _artist = infos.Artist;
            _duration = infos.Duration;
            _isLiked = infos.IsLiked;
            
            this.renameSong = renameSong;
            this.renameSong.ResultSended += (s, newNameResult) =>
            {
                if (newNameResult.IsSuccess)
                {
                    Title = newNameResult.GetValue();
                }
            };

            this.changeFavouriteState = changeFavouriteState;

            
        }

        public void Rename(string newName)
        {
            var request = new RenameSongRequest(Id, newName);
            renameSong.Execute(request);
        }

        void ToggleFavourite()
        {
            var request = new ChangeFavouriteStateRequest(Id, !IsLiked);
            changeFavouriteState.Execute(request);
            IsLiked = !IsLiked;
        }

    }
}
