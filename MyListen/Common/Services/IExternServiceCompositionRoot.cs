using MyListen.Common.Services.Importers;
using MyListen.Common.Services.Stores;

namespace MyListen.Common.Services
{
    public interface IExternServiceCompositionRoot
    {
        ISongPlayer Player { get; }
        ISongStore SongStore { get; }
        IPlaylistStore PlaylistStore { get; }
        ISongImporter SongImporter { get; }
        IPlaylistImporter PlaylistImporter { get; }
    }
}
