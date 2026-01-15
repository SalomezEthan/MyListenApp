using LiteDB;
using MyListen.Common.Entities;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;
using MyListenInfra.Win.Rows;
using System.Diagnostics;

namespace MyListenInfra.Win
{
    internal sealed class LiteDbPlaylistStore : IPlaylistRepository
    {
        readonly LiteDatabase db;
        readonly ILiteCollection<PlaylistDataRow> collection;

        public LiteDbPlaylistStore(string sourcePath)
        {
            string fileDbPath = Path.Combine(sourcePath, "playlist.db");
            this.db = new LiteDatabase(fileDbPath);
            this.collection = db.GetCollection<PlaylistDataRow>();
        }

        public Playlist GetPlaylistById(Guid Id)
        {
            PlaylistDataRow row = collection.FindById(Id);
            return row.ToEntity();
        }

        public IReadOnlyList<Playlist> CollectAll()
        {
            var rows = collection.FindAll();
            return [.. rows.Select(row => row.ToEntity())];
        }

        public void AddPlaylist(Playlist playlist, bool isReadOnly = false)
        {
            var row = PlaylistDataRow.FromEntity(playlist, isReadOnly);
            collection.Insert(row);
        }

        public void UpdatePlaylist(Playlist playlist)
        {
            var updateRow = PlaylistDataRow.FromEntity(playlist, playlist.IsReadonly);
            collection.Update(updateRow);
        }

        public bool RemoveById(Guid playlistId)
        {
            return collection.Delete(playlistId);
        }

        public bool PlaylistExistsByName(Name name)
        {
            return collection.FindAll().Any(playlist => name == playlist.Name);
        }
    }
}
