using MyListen.Common.Services.Importers;
using MyListen.Common.Services.Stores;

namespace MyListen.Common.Services
{
    public interface IExternServiceCompositionRoot
    {
        IMusicPlayer MusicPlayer { get; }
        IMusicStore MusicStore { get; }
        IPlaylistStore PlaylistStore { get; }
        IMusicImporter MusicImporter { get; }
        IPlaylistImporter PlaylistImporter { get; }
    }
}
