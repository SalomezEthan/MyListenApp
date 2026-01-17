using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Documents;
using MyArchitecture;
using MyArchitecture.PresenterLayer;
using MyListen.Player;
using MyListenApp.Services;
using MyListenApp.ViewModels.Song;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;

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

        public ICommand TogglePlayPauseCommand { get; }
        public ICommand PreviousCommand { get; }
        public ICommand NextCommand { get; }
        public ICommand ToggleShuffleCommand { get; }
        public ICommand ToggleLoopCommand { get; }


        readonly PlayerService playerService;

        public PlayerViewModel(PlayerService player, SongViewModelMap songViewModelMap)
        {
            this.playerService = player;
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

            TogglePlayPauseCommand = new RelayCommand(() => playerService.SwitchPlaybackState(IsPlaying));

            PreviousCommand = new RelayCommand(() =>
            {
                Result result = playerService.PlayPreviousSong();
                if (!result.IsSuccess) Debug.WriteLine(result.GetFailure());
            });

            NextCommand = new RelayCommand(() =>
            {
                Result result = playerService.PlayNextSong();
                if (!result.IsSuccess) Debug.WriteLine(result.GetFailure());
            });

            ToggleShuffleCommand = new RelayCommand(() =>
            {
                Guid startMusicId = CurrentSong?.Id ?? Queue.First().Id;
                if (IsShuffled) playerService.OrderPlaybackQueue(startMusicId);
                else playerService.ShufflePlaybackQueue(startMusicId);
            });

        }

        void ChangeVolume(float volume)
        {
            playerService.ChangeVolume(volume);
        }
    }

}
