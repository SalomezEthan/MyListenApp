using MyArchitecture.ApplicationLayer;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Services.Stores;

namespace MyListen.SongList.UseCases;

public sealed record CollectMusicsRequest(Guid PlaylistId);

public sealed record CollectMusicsResponse(IReadOnlyList<MusicInfos> CollectedMusics);

public sealed class CollectMusics(IMusicStore musicStore, IPlaylistStore playlistStore)
: UseCase<CollectMusicsRequest, CollectMusicsResponse>
{
    readonly IMusicStore musicStore = musicStore;
    readonly IPlaylistStore playlistStore = playlistStore;

    public override void Execute(CollectMusicsRequest request)
    {
        var playlist = playlistStore.GetFromPlaylistId(request.PlaylistId);
        var musics = musicStore.GetMusicsFromIds([.. playlist.MusicIds]);
        var musicInfos = from music in musics select MusicInfos.FromMusicEntity(music);
        Send(new CollectMusicsResponse([.. musicInfos]));
    }
}
