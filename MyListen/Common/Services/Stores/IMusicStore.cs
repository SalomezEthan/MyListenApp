using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.ValueObjects;

namespace MyListen.Common.Services.Stores
{
    public interface IMusicStore
    {
        Music GetByMusicId(Guid id);
        IReadOnlyList<Music> GetMusicsFromIds(Guid[] ids);
        IReadOnlyList<Music> CollectAll();

        void InsertMusic(ImportedMusic musicImported);
        void UpdateMusic(Music music);

        Reference GetReferenceById(Guid id);
        bool CheckIfMusicExistsByReference(Reference musicReference);
    }
}
