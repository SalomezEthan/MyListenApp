using MyArchitecture;
using MyListen.Common.Services.Stores;
using MyListen.Song.UseCases;

namespace MyListen.Song;

public sealed class SongComposantFactory(IMusicStore musicStore)
{
    readonly IMusicStore musicStore = musicStore;

    public ChangeFavouriteState CreateChangeFavouriteState()
    {
        return new ChangeFavouriteState(musicStore);
    }

    public RenameMusic CreateRenameMusic()
    {
        return new RenameMusic(musicStore);
    }
}
