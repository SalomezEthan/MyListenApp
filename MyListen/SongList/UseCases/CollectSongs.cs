using MyArchitecture.ApplicationLayer;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Services.Stores;

namespace MyListen.SongList.UseCases;

public sealed record CollectSongsRequest(Guid PlaylistId);

public sealed record CollectSongsResponse(IReadOnlyList<SongInfos> CollectedSongs);

public sealed class CollectSongs(ISongStore songStore, IPlaylistStore playlistStore)
: UseCase<CollectSongsRequest, CollectSongsResponse>
{
    readonly ISongStore songStore = songStore;
    readonly IPlaylistStore playlistStore = playlistStore;

    public override void Execute(CollectSongsRequest request)
    {
        var playlist = playlistStore.GetFromPlaylistId(request.PlaylistId);
        var songs = songStore.GetSongsFromIds([.. playlist.SongIds]);
        var songInfos = from song in songs select SongInfos.FromSongEntity(song);
        Send(new CollectSongsResponse([.. songInfos]));
    }
}
