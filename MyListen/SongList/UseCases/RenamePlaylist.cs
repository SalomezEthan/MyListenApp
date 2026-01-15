using MyArchitecture;
using MyArchitecture.ApplicationLayer;
using MyArchitecture.ApplicationLayer.UseCases.Sync;
using MyListen.Common.Entities;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.SongList.UseCases;

public sealed record RenamePlaylistRequest(Guid PlaylistId, string NewName);

public sealed class RenamePlaylist(IPlaylistRepository playlistStore) 
: UseCase<RenamePlaylistRequest, Result<string>>
{
    readonly IPlaylistRepository playlistStore = playlistStore;

    public override void Execute(RenamePlaylistRequest request)
    {
        Playlist playlist = playlistStore.GetPlaylistById(request.PlaylistId);
        Result<Name> newName = Name.FromString(request.NewName);
        if (!newName.IsSuccess)
        {
            Send(Result<string>.Fail($"Impossible d'utiliser ce nom : {newName.GetFailure()}"));
            return;
        }

        var result = playlist.Rename(newName.GetValue());
        if (!result.IsSuccess)
        {
            Send(Result<string>.Fail($"Le renommage a échoué : {result.GetFailure()}"));
            return;
        }

        playlistStore.UpdatePlaylist(playlist);
        Send(Result<string>.Ok(playlist.Name.ToString()));
    }
}
