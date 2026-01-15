using MyListen.Common.ValueObjects;

namespace MyListen.Common.Services.Stores
{
    public interface ISongListRepository
    {
        Entities.SongList GetSongListById(Guid Id);
        IReadOnlyList<Entities.SongList> CollectAll();

        void AddSongList(Entities.SongList songList, bool isReadOnly = false);
        void UpdateSongList(Entities.SongList songList);
        bool RemoveById(Guid songListId);

        bool SongListExistsByName(Name name);
    }
}
