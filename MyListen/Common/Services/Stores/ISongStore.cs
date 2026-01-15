using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.ValueObjects;

namespace MyListen.Common.Services.Stores
{
    public interface ISongStore
    {
        Entities.Song GetBySongId(Guid songId);
        IReadOnlyList<Entities.Song> GetSongsFromIds(Guid[] songIds);
        IReadOnlyList<Entities.Song> CollectAll();

        void InsertSong(ImportedSong songImported);
        void UpdateSong(Entities.Song song);

        Reference GetReferenceById(Guid songId);
        bool SongExistsByReference(Reference songReference);
    }
}
