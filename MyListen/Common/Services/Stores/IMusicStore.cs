using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.ValueObjects;

namespace MyListen.Common.Services.Stores
{
    public interface IMusicStore
    {
        Entities.Song GetByMusicId(Guid id);
        IReadOnlyList<Entities.Song> GetMusicsFromIds(Guid[] ids);
        IReadOnlyList<Entities.Song> CollectAll();

        void InsertMusic(ImportedMusic musicImported);
        void UpdateMusic(Entities.Song music);

        Reference GetReferenceById(Guid id);
        bool CheckIfMusicExistsByReference(Reference musicReference);
    }
}
