using MyArchitecture;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Services.Importers;
using MyListen.Common.Services.Stores;
using MyListen.Common.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyListen.Library
{
    public sealed class LibraryService(ISongListRepository songListRepo, ISongListImporter songListImporter, ISongRespository songRepository, ISongImporter songImporter)
    {
        readonly ISongListRepository songListRepo = songListRepo;
        readonly ISongListImporter songListImporter = songListImporter;
        readonly ISongRespository songRepo = songRepository;
        readonly ISongImporter songImporter = songImporter;

        public Result<SongListInfos> ImportSongList(string songListReference)
        {
            var reference = Reference.FromString(songListReference);
            if (!reference.IsSuccess) return Result<SongListInfos>.Fail($"La référence est invalide : {reference.GetFailure().BrokenRule}");

            var songList = songListImporter.ImportFromSongListReference(reference.GetValue());
            if (songListRepo.SongListExistsByName(songList.Name)) return Result<SongListInfos>.Fail("Cette liste existe déjà");

            var songs = songImporter.ImportSongsFromSource(reference.GetValue());
            foreach(var song in songs)
            {
                songRepo.AddSong(song);
                songList.AddNewSongsId(song.Entity.Id);
            }

            songListRepo.AddSongList(songList);
            return Result<SongListInfos>.Ok(SongListInfos.FromEntity(songList));
        }

        public IReadOnlyList<SongListInfos> CollectSongs()
        {
            var songlists = songListRepo.CollectAll();
            return [.. songlists.Select(SongListInfos.FromEntity)];
        }
    }
}
