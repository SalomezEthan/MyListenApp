using MyArchitecture.ApplicationLayer;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Services.Stores;

namespace MyListen.SongList.UseCases;

public sealed record CollectSongsRequest(Guid PlaylistId);

public sealed record CollectSongsResponse(IReadOnlyList<SongInfos> CollectedSongs);

public sealed class CollectSongs(ISongRespository songStore, IPlaylistRepository playlistStore)
: UseCase<CollectSongsRequest, CollectSongsResponse>
{
    readonly ISongRespository songStore = songStore;
    readonly IPlaylistRepository playlistStore = playlistStore;

    public override void Execute(CollectSongsRequest request)
    {
        var playlist = playlistStore.GetPlaylistById(request.PlaylistId);
        var songs = songStore.GetSongsByIds([.. playlist.SongIds]);
        var songInfos = from song in songs select SongInfos.FromSongEntity(song);
        Send(new CollectSongsResponse([.. songInfos]));
    }
}
