
using MyArchitecture;
using MyListen.Common.Entities;
using MyListen.Common.Services.Importers;
using MyListen.Common.ValueObjects;

namespace MyListenInfra.Win.Importers
{
    internal sealed class IOSongListImporter : ISongListImporter
    {
        public SongList ImportFromSongListReference(Reference songListReference)
        {
            Result<Name> name = Name.FromString(Path.GetFileName(songListReference.ToString()));
            if (!name.IsSuccess)
            {
                throw new InvalidOperationException($"Le nom ne peut être initialisée : {name.GetFailure()}");
            }

            return new SongList(Guid.NewGuid(), name.GetValue(), [], false);
        }
    }
}
