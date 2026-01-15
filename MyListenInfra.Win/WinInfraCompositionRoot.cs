using MyListen.Common.Services;
using MyListen.Common.Services.Importers;
using MyListen.Common.Services.Stores;
using MyListenInfra.Win.Importers;

namespace MyListenInfra.Win
{
    public sealed class WinInfraCompositionRoot (string sourcePath) : IExternServiceCompositionRoot
    {
        public ISongPlayer Player { get; } = new NAudioSongPlayer();

        public ISongRespository SongRepo { get; } = new LiteDbSongRepository(sourcePath);

        public ISongListRepository SongListRepo { get; } = new LiteDbSongListRepository(sourcePath);

        public ISongImporter SongImporter { get; } = new TagLibSongImporter();

        public ISongListImporter SongListImporter { get; } = new IOSongListImporter();
    }
}
