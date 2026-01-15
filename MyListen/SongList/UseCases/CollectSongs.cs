using MyArchitecture.ApplicationLayer;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Services.Stores;

namespace MyListen.SongList.UseCases;

public sealed record CollectSongsRequest(Guid SongListId);

public sealed record CollectSongsResponse(IReadOnlyList<SongInfos> CollectedSongs);

public sealed class CollectSongs(ISongRespository songRepo, ISongListRepository songListRepo)
: UseCase<CollectSongsRequest, CollectSongsResponse>
{
    readonly ISongRespository songRepo = songRepo;
    readonly ISongListRepository songListRepo = songListRepo;

    public override void Execute(CollectSongsRequest request)
    {
        var songList = songListRepo.GetSongListById(request.SongListId);
        var songs = songRepo.GetSongsByIds([.. songList.SongIds]);
        var songInfos = from song in songs select SongInfos.FromSongEntity(song);
        Send(new CollectSongsResponse([.. songInfos]));
    }
}
