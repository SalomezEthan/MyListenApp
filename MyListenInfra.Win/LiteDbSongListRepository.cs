using LiteDB;
using MyListen.Common.Entities;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;
using MyListenInfra.Win.Rows;

namespace MyListenInfra.Win
{
    internal sealed class LiteDbSongListRepository : ISongListRepository
    {
        readonly LiteDatabase db;
        readonly ILiteCollection<SongListDataRow> collection;

        public LiteDbSongListRepository(string sourcePath)
        {
            string fileDbPath = Path.Combine(sourcePath, "songList.db");
            this.db = new LiteDatabase(fileDbPath);
            this.collection = db.GetCollection<SongListDataRow>();
        }

        public SongList GetSongListById(Guid Id)
        {
            SongListDataRow row = collection.FindById(Id);
            return row.ToEntity();
        }

        public IReadOnlyList<SongList> CollectAll()
        {
            var rows = collection.FindAll();
            return [.. rows.Select(row => row.ToEntity())];
        }

        public void AddSongList(SongList songList, bool isReadOnly = false)
        {
            var row = SongListDataRow.FromEntity(songList, isReadOnly);
            collection.Insert(row);
        }

        public void UpdateSongList(SongList songList)
        {
            var updateRow = SongListDataRow.FromEntity(songList, songList.IsReadonly);
            collection.Update(updateRow);
        }

        public bool RemoveById(Guid songListId)
        {
            return collection.Delete(songListId);
        }

        public bool SongListExistsByName(Name name)
        {
            return collection.FindAll().Any(songList => name == songList.Name);
        }
    }
}
