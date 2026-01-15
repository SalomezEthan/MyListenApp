using MyListen.Common.Services;
using MyListen.Common.Services.Importers;
using MyListen.Common.Services.Stores;
using MyListenInfra.Win.Importers;

namespace MyListenInfra.Win
{
    public sealed class WinInfraCompositionRoot (string sourcePath) : IExternServiceCompositionRoot
    {
        public ISongPlayer Player { get; } = new NAudioSongPlayer();

        public ISongRespository SongStore { get; } = new LiteDbSongRepository(sourcePath);

        public IPlaylistRepository PlaylistStore { get; } = new LiteDbPlaylistStore(sourcePath);

        public ISongImporter SongImporter { get; } = new TagLibSongImporter();

        public IPlaylistImporter PlaylistImporter { get; } = new IOPlaylistImporter();
    }
}
