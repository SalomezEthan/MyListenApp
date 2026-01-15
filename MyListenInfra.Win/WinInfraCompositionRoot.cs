using MyListen.Common.Services;
using MyListen.Common.Services.Importers;
using MyListen.Common.Services.Stores;
using MyListenInfra.Win.Importers;

namespace MyListenInfra.Win
{
    public sealed class WinInfraCompositionRoot (string sourcePath) : IExternServiceCompositionRoot
    {
        public IMusicPlayer MusicPlayer { get; } = new NAudioMusicPlayer();

        public IMusicStore MusicStore { get; } = new LiteDbMusicStore(sourcePath);

        public IPlaylistStore PlaylistStore { get; } = new LiteDbPlaylistStore(sourcePath);

        public IMusicImporter MusicImporter { get; } = new TagLibMusicImporter();

        public IPlaylistImporter PlaylistImporter { get; } = new IOPlaylistImporter();
    }
}
