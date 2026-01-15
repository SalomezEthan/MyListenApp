using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.ValueObjects;

namespace MyListen.Common.Services.Importers
{
    public interface IPlaylistImporter
    {
        Playlist ImportFromPlaylistReference(Reference playlistReference);
    }
}
