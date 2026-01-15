using MyArchitecture;
using MyArchitecture.ApplicationLayer;
using MyArchitecture.ApplicationLayer.UseCases.Sync;
using MyListen.Common.Entities;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.Song.UseCases;

public sealed record RenameMusicRequest(Guid MusicId, string NewName);

public sealed class RenameMusic(IMusicStore musicStore)
: UseCase<RenameMusicRequest, Result<string>>
{
    readonly IMusicStore musicStore = musicStore;

    public override void Execute(RenameMusicRequest request)
    {
        Common.Entities.Song music = musicStore.GetByMusicId(request.MusicId);
        Result<Name> newName = Name.FromString(request.NewName);
        if (!newName.IsSuccess)
        {
            Send(Result<string>.Fail($"Impossible d'utiliser ce nom : {newName.GetFailure()}"));
            return;
        }

        Result result = music.Rename(newName.GetValue());
        if (!result.IsSuccess)
        {
            Send(Result<string>.Fail($"Erreur lors du renommage de la musique {music.Id} : {result.GetFailure()}"));
            return;
        }

        musicStore.UpdateMusic(music);
        Send(Result<string>.Ok(music.Title.ToString()));
    }
}
