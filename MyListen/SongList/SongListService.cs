using MyArchitecture;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Services;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.SongList
{
    public sealed class SongListService(ISongRespository songRepo, ISongListRepository songListRepo, PlaybackQueue playbackQueue, ISongPlayer songPlayer)
    {
        readonly ISongRespository songRepo = songRepo;
        readonly ISongListRepository songListRepo = songListRepo;
        readonly PlaybackQueue playbackQueue = playbackQueue;

        public IReadOnlyList<SongInfos> CollectSongs(Guid songListId)
        {
            var songList = songListRepo.GetSongListById(songListId);
            var songs = songRepo.GetSongsByIds([.. songList.SongIds]);
            var songInfos = from song in songs select SongInfos.FromSongEntity(song);
            return [.. songInfos];
        }

        public Result<string> RenameSongList(Guid songListId, string newName) 
        {
            Common.Entities.SongList songList = songListRepo.GetSongListById(songListId);

            Result<Name> name = Name.FromString(newName);
            if (!name.IsSuccess) return Result<string>.Fail($"Impossible d'utiliser ce nom : {name.GetFailure()}");

            var result = songList.Rename(name.GetValue());
            if (!result.IsSuccess)return Result<string>.Fail($"Le renommage a échoué : {result.GetFailure()}");

            songListRepo.UpdateSongList(songList);
            return Result<string>.Ok(songList.Name.ToString());
        }

        public Result PlaySongList(Guid songListId, Guid? songStart = null)
        {
            Common.Entities.SongList songList = songListRepo.GetSongListById(songListId);
            Result<EnqueueList> enqueueList = EnqueueList.FromSongs(songList.SongIds);
            if (!enqueueList.IsSuccess) return Result.Fail($"Impossible de créer la liste de lecture : {enqueueList.GetFailure()}");

            var enqueueListValue = enqueueList.GetValue();
            playbackQueue.SetPlaybackQueue(enqueueListValue);

            Guid realSongStartId = songStart ?? enqueueListValue.Songs[0];
            playbackQueue.MoveTo(realSongStartId);

            Reference songReference = songRepo.GetSongReferenceById(realSongStartId);
            songPlayer.PlaySong(songReference);

            return Result.Ok();
        }
    }
}
