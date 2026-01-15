using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml.Documents;
using MyArchitecture.PresenterLayer;
using MyListen.Player.Listeners;
using MyListen.Player.UseCases;
using MyListenApp.ViewModels.Song;
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


        readonly ChangePlaybackState changePlaybackState;
        readonly NextMusicTrigger next;
        readonly PreviousMusicTrigger previous;
        readonly ChangeVolume changeVolume;

        public PlayerViewModel
        (
            ChangePlaybackState changePlaybackState,
            NextMusicTrigger next, 
            PreviousMusicTrigger previous, 
            ChangeVolume changeVolume, 
            MusicChangedListener musicChangedListener, 
            QueueChangedListener queueChangedListener,
            PlaybackStateChangedListener stateChangedListener,
            SongEndedListener songEndedListener,
            SongViewModelMap songViewModelMap
        )
        {
            this.changePlaybackState = changePlaybackState;
            this.next = next;
            this.previous = previous;
            this.changeVolume = changeVolume;

            queueChangedListener.Notified += (s, newQueue) =>
            {
                Queue = [.. newQueue.Select(infos => songViewModelMap.GetSafeWithSongInfos(infos))];
            };

            musicChangedListener.Notified += (s, newMusic) =>
            {
                CurrentSong = songViewModelMap.GetSafeWithSongInfos(newMusic);
            };

            stateChangedListener.Notified += (s, newPlayState) =>
            {
                IsPlaying = newPlayState.IsPlaying;
                App.BackgroundService.SetPlayState(newPlayState.IsPlaying);
            };

            songEndedListener.Notified += (s, e) =>
            {
                Next();
            };
        }

        public void TogglePlayPause()
        {
            var request = new ChangePlaybackStateRequest(IsPlaying);
            changePlaybackState.Execute(request);
        }

        public void Next() => next.Execute();
        public void Previous() => previous.Execute();

        public void ChangeVolume(float volume)
        {
            var request = new ChangeVolumeRequest(volume);
            changeVolume.Execute(request);
        }
    }

}
