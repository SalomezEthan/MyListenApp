using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.ValueObjects;

namespace MyListen.Common.Services.Importers
{
    public interface ISongImporter
    {
        ImportedSong ImportFromSongReference(Reference songReference);
        IReadOnlyList<ImportedSong> ImportSongsFromSource(Reference sourceReference);
    }
}
