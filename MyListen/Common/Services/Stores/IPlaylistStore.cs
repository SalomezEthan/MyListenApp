using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.ValueObjects;

namespace MyListen.Common.Services.Stores
{
    public interface IPlaylistStore
    {
        Playlist GetFromPlaylistId(Guid Id);
        IReadOnlyList<Playlist> CollectAll();

        void InsertPlaylist(Playlist playlist, bool isReadOnly = false);
        void UpdatePlaylist(Playlist playlist);

        bool PlaylistExistsByName(Name name);
    }
}
