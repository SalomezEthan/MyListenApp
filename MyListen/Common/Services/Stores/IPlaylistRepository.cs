using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.ValueObjects;

namespace MyListen.Common.Services.Stores
{
    public interface IPlaylistRepository
    {
        Playlist GetPlaylistById(Guid Id);
        IReadOnlyList<Playlist> CollectAll();

        void AddPlaylist(Playlist playlist, bool isReadOnly = false);
        void UpdatePlaylist(Playlist playlist);
        bool RemoveById(Guid playlistId);

        bool PlaylistExistsByName(Name name);
    }
}
