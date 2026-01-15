using MyArchitecture;
using MyArchitecture.ApplicationLayer.UseCases.Sync;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Services.Stores;

namespace MyListen.Library.UseCases;

public sealed record CollectedSongList(IReadOnlyList<SongListInfos> SongLists);

public sealed class CollectSongList(ISongListRepository songListRepo)
: TriggerUseCase<CollectedSongList>
{
    readonly ISongListRepository songListRepo = songListRepo;

    public override void Execute()
    {
        var songLists = songListRepo.CollectAll();
        var songListInfos = from songList in songLists
                            select new SongListInfos
                            {
                                Id = songList.Id,
                                Name = songList.Name.ToString(),
                                Count = songList.Count,
                            };

        Send(new CollectedSongList([..songListInfos]));
    }
}
