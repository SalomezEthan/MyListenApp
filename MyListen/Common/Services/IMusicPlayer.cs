using MyListen.Common.Entities;
using MyListen.Common.ValueObjects;

namespace MyListen.Common.Services
{
    public interface IMusicPlayer
    {
        void PlayMusic(Reference musicReference);
        void Play();
        void Pause();
        void ChangeVolume(Volume volume);

        event EventHandler<StateChangedEventArgs>? StateChanged;
        event EventHandler? SongEnded;
    }

    public sealed record StateChangedEventArgs(bool IsPlaying);
}
