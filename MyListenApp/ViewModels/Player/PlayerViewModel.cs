using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Documents;
using MyArchitecture.PresenterLayer;
using MyListen.Player;
using MyListenApp.ViewModels.Song;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace MyListenApp.ViewModels.Player
{
    internal sealed partial class PlayerViewModel : BaseViewModel
    {
        bool _isPlaying = false;
        public bool IsPlaying
        {
            get => _isPlaying;
            set => SetValue(ref _isPlaying, value);
        }

        SongViewModel? _currentSong;
        public SongViewModel? CurrentSong
        {
            get => _currentSong;
            set => SetValue(ref _currentSong, value);
        }

        ObservableCollection<SongViewModel> _queue = [];
        public ObservableCollection<SongViewModel> Queue
        {
            get => _queue;
            set => SetValue(ref _queue, value);
        }

        float _volume;
        public float Volume
        {
            get => _volume;
            set
            {
                SetValue(ref _volume, value);
                ChangeVolume(_volume);
            }
        }

        bool _isShuffled;
        public bool IsShuffled
        {
            get => _isShuffled;
            set => SetValue(ref _isShuffled, value);
        }

        bool _isLooped;
        public bool IsLooped
        {
            get => _isLooped;
            set => SetValue(ref _isLooped, value);
        }


        readonly PlayerService playerService;

        public PlayerViewModel(PlayerService playerService, SongViewModelMap songViewModelMap)
        {
            this.playerService = playerService;
            this.playerService.SongChanged += (s, e) =>
            {
                CurrentSong = songViewModelMap.GetSafeWithSongInfos(e);
            };

            this.playerService.PlaybackQueueChanged += (s, e) =>
            {
                var songViewModels = e.Select(songViewModelMap.GetSafeWithSongInfos);
                Queue = new ObservableCollection<SongViewModel>(songViewModels);
            };

            this.playerService.PlaybackStateChanged += (s, e) =>
            {
                IsPlaying = e;
                App.BackgroundService.SetPlayState(e);
            };

            this.playerService.ShuffleStateChanged += (s, e) =>
            {
                IsShuffled = e;
            };
        }

        public void TogglePlayPause() => playerService.SwitchPlaybackState(IsPlaying);
        public void PlayNextSong() => playerService.PlayNextSong();
        public void PlayPreviousSong() => playerService.PlayPreviousSong();
        public void ChangeVolume(float volume) => playerService.ChangeVolume(volume);

        public void ToggleShuffleOrder()
        {
            Guid startMusicId = CurrentSong?.Id ?? Queue.First().Id;
            if (IsShuffled) playerService.OrderPlaybackQueue(startMusicId);
            else playerService.ShufflePlaybackQueue(startMusicId);
        }
    }

}
