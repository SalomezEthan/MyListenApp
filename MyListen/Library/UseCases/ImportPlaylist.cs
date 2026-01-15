using MyArchitecture;
using MyArchitecture.ApplicationLayer;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.Services.Importers;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.Library.UseCases
{
    public sealed record ImportPlaylistRequest(string Reference);

    public sealed class ImportPlaylist(IPlaylistRepository playlistStore, ISongRespository songStore, IPlaylistImporter playlistImporter, ISongImporter songImporter)
    : UseCase<ImportPlaylistRequest, Result<PlaylistInfos>>
    {
        readonly IPlaylistRepository playlistStore = playlistStore;
        readonly ISongRespository songStore = songStore;
        readonly IPlaylistImporter playlistImporter = playlistImporter;
        readonly ISongImporter songImporter = songImporter;

        public override void Execute(ImportPlaylistRequest request)
        {
            Result<Reference> reference = Reference.FromString(request.Reference);
            if (!reference.IsSuccess)
            {
                Send(Result<PlaylistInfos>.Fail($"La référence de la playlist est invalide : {reference.GetFailure().BrokenRule}"));
                return;
            }


            Playlist playlist = playlistImporter.ImportFromPlaylistReference(reference.GetValue());
            if (playlistStore.PlaylistExistsByName(playlist.Name))
            {
                Send(Result<PlaylistInfos>.Fail("La playlist existe déjà."));
                return;
            }

            ImportPlaylistWithSongs(playlist, reference.GetValue());
            Send(Result<PlaylistInfos>.Ok(new PlaylistInfos
            {
                Id = playlist.Id,
                Name = playlist.Name.ToString(),
                Count = playlist.Count
            }));

        }

        private Playlist ImportPlaylistWithSongs(Playlist playlist, Reference reference)
        {
            playlistStore.AddPlaylist(playlist);
            var importedSongs = songImporter.ImportSongsFromSource(reference);
            foreach (var song in importedSongs)
            {
                songStore.AddSong(song);
                playlist.AddNewSongsId(song.Entity.Id);
            }

            playlistStore.UpdatePlaylist(playlist);
            return playlist;
        }
    }
}
