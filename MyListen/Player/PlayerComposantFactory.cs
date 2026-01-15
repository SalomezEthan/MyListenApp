using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.Player.Listeners;
using MyListen.Player.UseCases;

namespace MyListen.Player
{
    public sealed class PlayerComposantFactory(PlaybackQueue queue, IMusicPlayer musicPlayer, IMusicStore musicStore)
    {
        readonly PlaybackQueue queue = queue;
        readonly IMusicPlayer musicPlayer = musicPlayer;
        readonly IMusicStore musicStore = musicStore;

        public ChangePlaybackState CreateChangePlaybackState() => new(musicPlayer);
        public NextMusicTrigger CreateNextMusicTrigger() => new(queue, musicPlayer, musicStore);
        public PreviousMusicTrigger CreatePreviousMusicTrigger() => new(queue, musicPlayer, musicStore);
        public ChangeVolume CreateChangeVolume() => new(musicPlayer);

        public MusicChangedListener CreateMusicChangedListener() => new(queue, musicStore);
        public QueueChangedListener CreateQueueChangedListener() => new(queue, musicStore);
        public PlaybackStateChangedListener CreateStateChangedListener() => new(musicPlayer);
        public SongEndedListener CreateSongEndedListener() => new(musicPlayer);

    }
}
