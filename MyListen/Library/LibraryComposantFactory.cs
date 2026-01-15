using MyListen.Common.Services.Importers;
using MyListen.Common.Services.Stores;
using MyListen.Library.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyListen.Library
{
    public sealed class LibraryComposantFactory(ISongListRepository songListRepo, ISongRespository songRepo, ISongListImporter songListImporter, ISongImporter songImporter)
    {
        readonly ISongListRepository songListRepo = songListRepo;
        readonly ISongRespository songRepo = songRepo;
        readonly ISongListImporter songListImporter = songListImporter;
        readonly ISongImporter songImporter = songImporter;

        public CollectSongList CreateCollectSongList() => new(songListRepo);
        public ImportSongList CreateImportSongList() => new(songListRepo, songRepo, songListImporter, songImporter);
        
    }
}
