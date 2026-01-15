using LiteDB;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;
using MyListenInfra.Win.Rows;

namespace MyListenInfra.Win
{
    internal class LiteDbSongRepository : ISongRespository
    {
        readonly LiteDatabase db;
        readonly ILiteCollection<SongDataRow> songCollection;
        public LiteDbSongRepository(string sourcePath)
        {
            string fileDbPath = Path.Combine(sourcePath, "song.db");
            this.db = new LiteDatabase(fileDbPath);
            this.songCollection = db.GetCollection<SongDataRow>();
        }

        public IReadOnlyList<Song> GetSongsByIds(Guid[] ids)
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

        public IReadOnlyList<Song> CollectFavouriteSongs()
        {
            var rows = songCollection.Find(musicRow => musicRow.IsLiked);
            return [.. rows.Select(row => row.ToEntity())];
        }

        public IReadOnlyList<Song> CollectAll()
        {
            var all = songCollection.FindAll();
            return [.. all.Select(row => row.ToEntity())];
        }

        public void AddSong(ImportedSong importedSong)
        {
            var newRow = SongDataRow.FromEntity(importedSong.Entity, importedSong.SongReference.ToString());
            songCollection.Insert(newRow);
        }

        public bool RemoveSongById(Guid songId)
        {
            return songCollection.Delete(songId);
        }

        public void UpdateSong(Song song)
        {
            string reference = songCollection.FindById(song.Id).Reference;
            var row = SongDataRow.FromEntity(song, reference);
            songCollection.Update(row);
        }

        public Reference GetSongReferenceById(Guid id)
        {
            var row = songCollection.FindById(id);
            return Reference.FromString(row.Reference).GetValue();
        }

        public bool SongExistsByReference(Reference songReference)
        {
            return songCollection.Exists(song =>  songReference == song.Reference);
        }

        

        public string SearchSongsByName(string chars)
        {
            throw new NotImplementedException();
        }
    }
}
