using LiteDB;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;
using MyListenInfra.Win.Rows;

namespace MyListenInfra.Win
{
    internal class LiteDbMusicStore : IMusicStore
    {
        readonly LiteDatabase db;
        readonly ILiteCollection<MusicDataRow> musicCollection;
        public LiteDbMusicStore(string sourcePath)
        {
            string fileDbPath = Path.Combine(sourcePath, "music.db");
            this.db = new LiteDatabase(fileDbPath);
            this.musicCollection = db.GetCollection<MusicDataRow>();
        }

        public IReadOnlyList<Music> GetMusicsFromIds(Guid[] ids)
        {
            List<Music> musics = [];
            foreach (Guid id in ids)
            {
                musics.Add(GetByMusicId(id));
            }

            return musics;
        }

        public Music GetByMusicId(Guid id)
        {
            var row = musicCollection.FindById(id);
            return row.ToEntity();
        }

        public IReadOnlyList<Music> CollectAll()
        {
            var all = musicCollection.FindAll();
            return [.. all.Select(row => row.ToEntity())];
        }

        public void InsertMusic(ImportedMusic importedMusic)
        {
            var newRow = MusicDataRow.FromEntity(importedMusic.Entity, importedMusic.MusicReference.ToString());
            musicCollection.Insert(newRow);
        }

        public void UpdateMusic(Music music)
        {
            string reference = musicCollection.FindById(music.Id).Reference;
            var row = MusicDataRow.FromEntity(music, reference);
            musicCollection.Update(row);
        }

        public Reference GetReferenceById(Guid id)
        {
            var row = musicCollection.FindById(id);
            return Reference.FromString(row.Reference).GetValue();
        }

        public bool CheckIfMusicExistsByReference(Reference musicReference)
        {
            return musicCollection.Exists(music =>  musicReference == music.Reference);
        }
    }
}
