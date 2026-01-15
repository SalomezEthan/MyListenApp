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
    public sealed class LibraryComposantFactory(IPlaylistStore playlistStore, ISongStore songStore, IPlaylistImporter playlistImporter, ISongImporter songImporter)
    {
        readonly IPlaylistStore playlistStore = playlistStore;
        readonly ISongStore songStore = songStore;
        readonly IPlaylistImporter playlistImporter = playlistImporter;
        readonly ISongImporter songImporter = songImporter;

        public CollectPlaylistTrigger CreateCollectPlaylist() => new(playlistStore);
        public ImportPlaylist CreateImportPlaylist() => new(playlistStore, songStore, playlistImporter, songImporter);
        
    }
}
