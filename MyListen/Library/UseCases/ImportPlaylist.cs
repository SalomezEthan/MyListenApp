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

    public sealed class ImportPlaylist(IPlaylistStore playlistStore, IMusicStore musicStore, IPlaylistImporter playlistImporter, IMusicImporter musicImporter)
    : UseCase<ImportPlaylistRequest, Result<PlaylistInfos>>
    {
        readonly IPlaylistStore playlistStore = playlistStore;
        readonly IMusicStore musicStore = musicStore;
        readonly IPlaylistImporter playlistImporter = playlistImporter;
        readonly IMusicImporter musicImporter = musicImporter;

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

            ImportPlaylistWithMusics(playlist, reference.GetValue());
            Send(Result<PlaylistInfos>.Ok(new PlaylistInfos
            {
                Id = playlist.Id,
                Name = playlist.Name.ToString(),
                Count = playlist.Count
            }));

        }

        private Playlist ImportPlaylistWithMusics(Playlist playlist, Reference reference)
        {
            playlistStore.InsertPlaylist(playlist);
            var importedMusics = musicImporter.ImportMusicsFromSource(reference);
            foreach (var music in importedMusics)
            {
                musicStore.InsertMusic(music);
                playlist.AddNewMusicId(music.Entity.Id);
            }

            playlistStore.UpdatePlaylist(playlist);
            return playlist;
        }
    }
}
