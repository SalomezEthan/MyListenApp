using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.ValueObjects;

namespace MyListen.Common.Services.Importers
{
    public interface IMusicImporter
    {
        ImportedMusic ImportFromMusicReference(Reference musicReference);
        IReadOnlyList<ImportedMusic> ImportMusicsFromSource(Reference sourceReference);
    }
}
