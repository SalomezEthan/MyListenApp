using MyArchitecture;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.Song
{
    public sealed class SongService(ISongRespository songRepo)
    {
        readonly ISongRespository songRepo = songRepo;

        public void ChangeFavouriteState(Guid songId, bool isFavourite)
        {
            Common.Entities.Song song = songRepo.GetBySongId(songId);
            if (!isFavourite) song.Like();
            else song.Dislike();
            songRepo.UpdateSong(song);
        }

        public Result RenameSong(Guid songId, string newName)
        {
            Common.Entities.Song song = songRepo.GetBySongId(songId);
            Result<Name> name = Name.FromString(newName);
            if (!name.IsSuccess) return Result.Fail($"Impossible d'utiliser ce nom : {name.GetFailure()}");

            Result result = song.Rename(name.GetValue());
            if (!result.IsSuccess) return Result.Fail($"Erreur lors du renommage de la musique {song.Id} : {result.GetFailure()}");

            songRepo.UpdateSong(song);
            return Result.Ok();
        }
    }
}
