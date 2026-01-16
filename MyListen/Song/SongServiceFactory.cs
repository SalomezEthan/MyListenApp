using MyListen.Common.Services.Stores;

namespace MyListen.Song;

public sealed class SongServiceFactory(ISongRespository songRepo)
{
    readonly ISongRespository songRepo = songRepo;

    public SongService CreateSongService()
    {
        return new SongService(songRepo);
    }
}
