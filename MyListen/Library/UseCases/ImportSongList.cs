using MyArchitecture;
using MyArchitecture.ApplicationLayer;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.Services.Importers;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;

namespace MyListen.Library.UseCases
{
    public sealed record ImportSongListRequest(string Reference);

    public sealed class ImportSongList(ISongListRepository songListRepo, ISongRespository songRepo, ISongListImporter songListImporter, ISongImporter songImporter)
    : UseCase<ImportSongListRequest, Result<SongListInfos>>
    {
        readonly ISongListRepository songListRepo = songListRepo;
        readonly ISongRespository songRepo = songRepo;
        readonly ISongListImporter songListImporter = songListImporter;
        readonly ISongImporter songImporter = songImporter;

        public override void Execute(ImportSongListRequest request)
        {
            Result<Reference> reference = Reference.FromString(request.Reference);
            if (!reference.IsSuccess)
            {
                Send(Result<SongListInfos>.Fail($"La référence de la songList est invalide : {reference.GetFailure().BrokenRule}"));
                return;
            }


            Common.Entities.SongList songList = songListImporter.ImportFromSongListReference(reference.GetValue());
            if (songListRepo.SongListExistsByName(songList.Name))
            {
                Send(Result<SongListInfos>.Fail("La songList existe déjà."));
                return;
            }

            ImportSongListWithSongs(songList, reference.GetValue());
            Send(Result<SongListInfos>.Ok(new SongListInfos
            {
                Id = songList.Id,
                Name = songList.Name.ToString(),
                Count = songList.Count
            }));

        }

        private Common.Entities.SongList ImportSongListWithSongs(Common.Entities.SongList songList, Reference reference)
        {
            songListRepo.AddSongList(songList);
            var importedSongs = songImporter.ImportSongsFromSource(reference);
            foreach (var song in importedSongs)
            {
                songRepo.AddSong(song);
                songList.AddNewSongsId(song.Entity.Id);
            }

            songListRepo.UpdateSongList(songList);
            return songList;
        }
    }
}
