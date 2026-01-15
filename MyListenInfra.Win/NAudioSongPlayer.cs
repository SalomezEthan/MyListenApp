using MyListen.Common.Services;
using MyListen.Common.ValueObjects;
using NAudio.Wave;

namespace MyListenInfra.Win
{
    internal sealed class NAudioSongPlayer : ISongPlayer
    {
        readonly WaveOutEvent player = new();
        Mp3FileReader? reader;

        public NAudioSongPlayer()
        {
            player.PlaybackStopped += (s, e) =>
            {
                if (player.PlaybackState != PlaybackState.Playing)
                {
                    OnSongEnded();
                }
            };
        }

        public void PlaySong(Reference reference)
        {
            player.Stop();
            reader?.Dispose();

            reader = new Mp3FileReader(reference.ToString());
            player.Init(reader);

            player.Play();
            OnStateChanged(true);
        }

        public void Play()
        {
            player.Play();
            OnStateChanged(true);
        }

        public void Pause()
        {
            player.Pause();
            OnStateChanged(false);
        }

        public void ChangeVolume(Volume volume)
        {
            player.Volume = volume.Value;
        }

        public event EventHandler<StateChangedEventArgs>? StateChanged;
        void OnStateChanged(bool isPlaying)
        {
            StateChanged?.Invoke(this, new StateChangedEventArgs(isPlaying));
        }

        public event EventHandler? SongEnded;
        void OnSongEnded()
        {
            SongEnded?.Invoke(this, EventArgs.Empty);
        }
    }
}
