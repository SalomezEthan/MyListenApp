using MyListen.Common.Services.Importers;
using MyListen.Common.Services.Stores;

namespace MyListen.Common.Services
{
    public interface IExternServiceCompositionRoot
    {
        ISongPlayer Player { get; }
        ISongRespository SongRepo { get; }
        ISongListRepository SongListRepo { get; }
        ISongImporter SongImporter { get; }
        ISongListImporter SongListImporter { get; }
    }
}
