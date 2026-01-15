using MyListen.Common.Entities;
using MyListen.Common.ValueObjects;

namespace MyListen.Common.Services
{
    public interface ISongPlayer
    {
        void PlaySong(Reference songReference);
        void Play();
        void Pause();
        void ChangeVolume(Volume volume);

        event EventHandler<StateChangedEventArgs>? StateChanged;
        event EventHandler? SongEnded;
    }

    public sealed record StateChangedEventArgs(bool IsPlaying);
}
