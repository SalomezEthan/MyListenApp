using MyArchitecture;
using MyArchitecture.ApplicationLayer;
using MyArchitecture.ApplicationLayer.UseCases.Sync;
using MyListen.Common.Entities;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.Song.UseCases;

public sealed record RenameSongRequest(Guid SongId, string NewName);

public sealed class RenameSong(ISongStore songStore)
: UseCase<RenameSongRequest, Result<string>>
{
    readonly ISongStore songStore = songStore;

    public override void Execute(RenameSongRequest request)
    {
        Common.Entities.Song song = songStore.GetBySongId(request.SongId);
        Result<Name> newName = Name.FromString(request.NewName);
        if (!newName.IsSuccess)
        {
            Send(Result<string>.Fail($"Impossible d'utiliser ce nom : {newName.GetFailure()}"));
            return;
        }

        Result result = song.Rename(newName.GetValue());
        if (!result.IsSuccess)
        {
            Send(Result<string>.Fail($"Erreur lors du renommage de la musique {song.Id} : {result.GetFailure()}"));
            return;
        }

        songStore.UpdateSong(song);
        Send(Result<string>.Ok(song.Title.ToString()));
    }
}
