using MyArchitecture;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.Player
{
    public sealed class PlayerService
    {
        readonly ISongPlayer player;
        readonly ISongRespository songRepo;
        readonly PlaybackQueue playbackQueue;

        public PlayerService(ISongPlayer player, ISongRespository songRepo, PlaybackQueue playbackQueue)
        {
            this.player = player;
            this.songRepo = songRepo;
            this.playbackQueue = playbackQueue;

            this.playbackQueue.PlaybackQueueChanged += (s, e) =>
            {
                var songs = songRepo.GetSongsByIds([.. e]);
                IReadOnlyList<SongInfos> songInfos = [.. songs.Select(SongInfos.FromSongEntity)];
                OnPlaybackQueueChanged(songInfos);
            };

            this.playbackQueue.CurrentSongIdChanged += (s, e) =>
            {
                Common.Entities.Song song = songRepo.GetBySongId(e);
                var infos = SongInfos.FromSongEntity(song);
                OnSongChanged(infos);
            };

            this.player.SongEnded += (s, e) =>
            {
                OnSongEnded();
            };

            this.player.StateChanged += (s, e) =>
            {
                OnPlaybackStateChanged(e.IsPlaying);
            };

            this.playbackQueue.IsShuffledStateChanged += (s, e) =>
            {
                OnShuffleStateChanged(e);
            };
        }

        public void SwitchPlaybackState(bool isPlaying)
        {
            if (!isPlaying) player.Play();
            else player.Pause();
        }

        public Result PlayNextSong()
        {
            Result<Guid> nextSongId = playbackQueue.NextSong();
            if (!nextSongId.IsSuccess) return Result.Fail($"Erreur lors du passage au son suivant : {nextSongId.GetFailure().BrokenRule}");

            PlaySongById(nextSongId.GetValue());
            return Result.Ok();
        }

        public Result PlayPreviousSong()
        {
            Result<Guid> previousSongId = playbackQueue.PreviousSong();
            if (!previousSongId.IsSuccess) return Result.Fail($"Erreur lors du passage au song précédent : {previousSongId.GetFailure()}");

            PlaySongById(previousSongId.GetValue());
            return Result.Ok();
        }

        void PlaySongById(Guid songId)
        {
            Reference songReference = songRepo.GetSongReferenceById(songId);
            player.PlaySong(songReference);
        }

        public Result ShufflePlaybackQueue(Guid startId)
        {
            Result result = playbackQueue.ShufflePlayBackQueue(startId);
            if (!result.IsSuccess) return Result.Fail($"Impossible de mélanger la file de lecture : {result.GetFailure().BrokenRule}");
            else return Result.Ok();
        }

        public Result OrderPlaybackQueue(Guid startId)
        {
            var result = playbackQueue.OrderPlaybackQueue();
            if (!result.IsSuccess) return Result.Fail($"Impossible d'ordonner la file de lecture : {result.GetFailure().BrokenRule}");

            playbackQueue.MoveTo(startId);
            return Result.Ok();
        }

        public Result ChangeVolume(float newVolume)
        {
            Result<Volume> volume = Volume.FromFloat(newVolume);
            if (!volume.IsSuccess) return Result.Fail($"La valeur utilisée pour le volume est incorrecte : {volume.GetFailure().BrokenRule}");

            player.ChangeVolume(volume.GetValue());
            return Result.Ok();
        }

        public event EventHandler<IReadOnlyList<SongInfos>>? PlaybackQueueChanged;
        void OnPlaybackQueueChanged(IReadOnlyList<SongInfos> newQueue)
        {
            PlaybackQueueChanged?.Invoke(this, newQueue);
        }

        public event EventHandler<SongInfos>? SongChanged;
        void OnSongChanged(SongInfos infos)
        {
            SongChanged?.Invoke(this, infos);
        }

        public event EventHandler<bool>? PlaybackStateChanged;
        void OnPlaybackStateChanged(bool isPlaying)
        {
            PlaybackStateChanged?.Invoke(this, isPlaying);
        }

        public event EventHandler? SongEnded;
        void OnSongEnded()
        {
            SongEnded?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<bool>? ShuffleStateChanged;
        void OnShuffleStateChanged(bool isShuffled)
        {
            ShuffleStateChanged?.Invoke(this, isShuffled);
        }
    }
}
