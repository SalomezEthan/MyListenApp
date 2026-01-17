using MyListen.Common.Services.Importers;
using MyListen.Common.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyListen.Library
{
    public sealed class LibraryServiceFactory(ISongListRepository songListRepo, ISongRespository songRepo, ISongListImporter songListImporter, ISongImporter songImporter)
    {
        readonly ISongListRepository songListRepo = songListRepo;
        readonly ISongRespository songRepo = songRepo;
        readonly ISongListImporter songListImporter = songListImporter;
        readonly ISongImporter songImporter = songImporter;

        public LibraryService CreateLibraryService()
        {
            return new LibraryService(songListRepo, songListImporter, songRepo, songImporter);
        }
    }
}
