using LiteDB;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;
using MyListenInfra.Win.Rows;

namespace MyListenInfra.Win
{
    internal class LiteDbSongStore : ISongStore
    {
        readonly LiteDatabase db;
        readonly ILiteCollection<SongDataRow> songCollection;
        public LiteDbSongStore(string sourcePath)
        {
            string fileDbPath = Path.Combine(sourcePath, "song.db");
            this.db = new LiteDatabase(fileDbPath);
            this.songCollection = db.GetCollection<SongDataRow>();
        }

        public IReadOnlyList<Song> GetSongsFromIds(Guid[] ids)
        {
            List<Song> songs = [];
            foreach (Guid id in ids)
            {
                songs.Add(GetBySongId(id));
            }

            return songs;
        }

        public Song GetBySongId(Guid id)
        {
            var row = songCollection.FindById(id);
            return row.ToEntity();
        }

        public IReadOnlyList<Song> CollectAll()
        {
            var all = songCollection.FindAll();
            return [.. all.Select(row => row.ToEntity())];
        }

        public void InsertSong(ImportedSong importedSong)
        {
            var newRow = SongDataRow.FromEntity(importedSong.Entity, importedSong.SongReference.ToString());
            songCollection.Insert(newRow);
        }

        public void UpdateSong(Song song)
        {
            string reference = songCollection.FindById(song.Id).Reference;
            var row = SongDataRow.FromEntity(song, reference);
            songCollection.Update(row);
        }

        public Reference GetReferenceById(Guid id)
        {
            var row = songCollection.FindById(id);
            return Reference.FromString(row.Reference).GetValue();
        }

        public bool SongExistsByReference(Reference songReference)
        {
            return songCollection.Exists(song =>  songReference == song.Reference);
        }
    }
}
