using MyArchitecture;
using MyArchitecture.ApplicationLayer.UseCases.Sync;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Services.Stores;

namespace MyListen.Library.UseCases;

public sealed record CollectedPlaylist(IReadOnlyList<PlaylistInfos> Playlists);

public sealed class CollectPlaylistTrigger(IPlaylistRepository playlistStore)
: TriggerUseCase<CollectedPlaylist>
{
    readonly IPlaylistRepository playlistCollector = playlistStore;

    public override void Execute()
    {
        var playlists = playlistCollector.CollectAll();
        var playlistInfos = from playlist in playlists
                            select new PlaylistInfos
                            {
                                Id = playlist.Id,
                                Name = playlist.Name.ToString(),
                                Count = playlist.Count,
                            };

        Send(new CollectedPlaylist([..playlistInfos]));
    }
}
