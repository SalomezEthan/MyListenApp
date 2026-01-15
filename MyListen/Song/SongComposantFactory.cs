using MyArchitecture;
using MyListen.Common.Services.Stores;
using MyListen.Song.UseCases;

namespace MyListen.Song;

public sealed class SongComposantFactory(ISongRespository songStore)
{
    readonly ISongRespository songStore = songStore;

    public ChangeFavouriteState CreateChangeFavouriteState()
    {
        return new ChangeFavouriteState(songStore);
    }

    public RenameSong CreateRenameSong()
    {
        return new RenameSong(songStore);
    }
}
