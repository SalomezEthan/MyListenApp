using MyArchitecture;
using MyListen.Common.Services.Stores;
using MyListen.Song.UseCases;

namespace MyListen.Song;

public sealed class SongComposantFactory(ISongRespository songRepo)
{
    readonly ISongRespository songRepo = songRepo;

    public ChangeFavouriteState CreateChangeFavouriteState()
    {
        return new ChangeFavouriteState(songRepo);
    }

    public RenameSong CreateRenameSong()
    {
        return new RenameSong(songRepo);
    }
}
