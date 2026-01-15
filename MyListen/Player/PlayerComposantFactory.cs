using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.Player.Listeners;
using MyListen.Player.UseCases;

namespace MyListen.Player
{
    public sealed class PlayerComposantFactory(PlaybackQueue queue, ISongPlayer songPlayer, ISongStore songStore)
    {
        readonly PlaybackQueue queue = queue;
        readonly ISongPlayer songPlayer = songPlayer;
        readonly ISongStore songStore = songStore;

        public ChangePlaybackState CreateChangePlaybackState() => new(songPlayer);
        public NextSongTrigger CreateNextSongTrigger() => new(queue, songPlayer, songStore);
        public PreviousSongTrigger CreatePreviousSongTrigger() => new(queue, songPlayer, songStore);
        public ChangeVolume CreateChangeVolume() => new(songPlayer);

        public SongChangedListener CreateSongChangedListener() => new(queue, songStore);
        public QueueChangedListener CreateQueueChangedListener() => new(queue, songStore);
        public PlaybackStateChangedListener CreateStateChangedListener() => new(songPlayer);
        public SongEndedListener CreateSongEndedListener() => new(songPlayer);

    }
}
