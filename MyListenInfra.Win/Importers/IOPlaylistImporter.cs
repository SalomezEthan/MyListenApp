
using MyArchitecture;
using MyListen.Common.Entities;
using MyListen.Common.Services.Importers;
using MyListen.Common.ValueObjects;

namespace MyListenInfra.Win.Importers
{
    internal sealed class IOPlaylistImporter : IPlaylistImporter
    {
        public Playlist ImportFromPlaylistReference(Reference playlistReference)
        {
            Result<Name> name = Name.FromString(Path.GetFileName(playlistReference.ToString()));
            if (!name.IsSuccess)
            {
                throw new InvalidOperationException($"Le nom ne peut être initialisée : {name.GetFailure()}");
            }

            return new Playlist(Guid.NewGuid(), name.GetValue(), [], false);
        }
    }
}
