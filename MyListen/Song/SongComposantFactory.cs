using MyArchitecture;
using MyListen.Common.Services.Stores;
using MyListen.Song.UseCases;

namespace MyListen.Song;

public sealed class SongComposantFactory(ISongStore songStore)
{
    readonly ISongStore songStore = songStore;

    public ChangeFavouriteState CreateChangeFavouriteState()
    {
        return new ChangeFavouriteState(songStore);
    }

    public RenameSong CreateRenameSong()
    {
        return new RenameSong(songStore);
    }
}
