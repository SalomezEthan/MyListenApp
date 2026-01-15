using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.ValueObjects;

namespace MyListen.Common.Services.Stores
{
    public interface ISongRespository
    {
        Entities.Song GetBySongId(Guid songId);
        IReadOnlyList<Entities.Song> GetSongsByIds(Guid[] songIds);

        IReadOnlyList<Entities.Song> CollectFavouriteSongs();
        IReadOnlyList<Entities.Song> CollectAll();

        void AddSong(ImportedSong songImported);
        void UpdateSong(Entities.Song song);
        bool RemoveSongById(Guid songId);

        Reference GetSongReferenceById(Guid songId);
        bool SongExistsByReference(Reference songReference);

        string SearchSongsByName(string chars);
    }
}
