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
    public sealed class LibraryComposantFactory(IPlaylistStore playlistStore, IMusicStore musicStore, IPlaylistImporter playlistImporter, IMusicImporter musicImporter)
    {
        readonly IPlaylistStore playlistStore = playlistStore;
        readonly IMusicStore musicStore = musicStore;
        readonly IPlaylistImporter playlistImporter = playlistImporter;
        readonly IMusicImporter musicImporter = musicImporter;

        public CollectPlaylistTrigger CreateCollectPlaylist() => new(playlistStore);
        public ImportPlaylist CreateImportPlaylist() => new(playlistStore, musicStore, playlistImporter, musicImporter);
        
    }
}
