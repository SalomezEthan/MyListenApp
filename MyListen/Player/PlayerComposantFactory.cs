using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.Player.Listeners;
using MyListen.Player.UseCases;

namespace MyListen.Player
{
    public sealed class PlayerComposantFactory(PlaybackQueue queue, ISongPlayer songPlayer, ISongRespository songRepo)
    {
        readonly PlaybackQueue queue = queue;
        readonly ISongPlayer songPlayer = songPlayer;
        readonly ISongRespository songRepo = songRepo;

        public ChangePlaybackState CreateChangePlaybackState() => new(songPlayer);
        public NextSongTrigger CreateNextSongTrigger() => new(queue, songPlayer, songRepo);
        public PreviousSongTrigger CreatePreviousSongTrigger() => new(queue, songPlayer, songRepo);
        public ChangeVolume CreateChangeVolume() => new(songPlayer);

        public SongChangedListener CreateSongChangedListener() => new(queue, songRepo);
        public QueueChangedListener CreateQueueChangedListener() => new(queue, songRepo);
        public PlaybackStateChangedListener CreateStateChangedListener() => new(songPlayer);
        public SongEndedListener CreateSongEndedListener() => new(songPlayer);

    }
}
