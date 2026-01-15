using MyArchitecture;
using MyArchitecture.ApplicationLayer;
using MyArchitecture.ApplicationLayer.UseCases.Sync;
using MyListen.Common.Entities;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.SongList.UseCases;

public sealed record RenameSongListRequest(Guid SongListId, string NewName);

public sealed class RenameSongList(ISongListRepository songListRepo) 
: UseCase<RenameSongListRequest, Result<string>>
{
    readonly ISongListRepository songListRepo = songListRepo;

    public override void Execute(RenameSongListRequest request)
    {
        Common.Entities.SongList songList = songListRepo.GetSongListById(request.SongListId);
        Result<Name> newName = Name.FromString(request.NewName);
        if (!newName.IsSuccess)
        {
            Send(Result<string>.Fail($"Impossible d'utiliser ce nom : {newName.GetFailure()}"));
            return;
        }

        var result = songList.Rename(newName.GetValue());
        if (!result.IsSuccess)
        {
            Send(Result<string>.Fail($"Le renommage a échoué : {result.GetFailure()}"));
            return;
        }

        songListRepo.UpdateSongList(songList);
        Send(Result<string>.Ok(songList.Name.ToString()));
    }
}
