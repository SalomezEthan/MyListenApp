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
    public sealed class LibraryComposantFactory(IPlaylistRepository playlistStore, ISongRespository songStore, IPlaylistImporter playlistImporter, ISongImporter songImporter)
    {
        readonly IPlaylistRepository playlistStore = playlistStore;
        readonly ISongRespository songStore = songStore;
        readonly IPlaylistImporter playlistImporter = playlistImporter;
        readonly ISongImporter songImporter = songImporter;

        public CollectPlaylistTrigger CreateCollectPlaylist() => new(playlistStore);
        public ImportPlaylist CreateImportPlaylist() => new(playlistStore, songStore, playlistImporter, songImporter);
        
    }
}
