using MyArchitecture;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.Services.Importers;
using MyListen.Common.ValueObjects;

namespace MyListenInfra.Win.Importers
{
    internal sealed class TagLibSongImporter : ISongImporter
    {
        public IReadOnlyList<ImportedSong> ImportSongsFromSource(Reference sourceReference)
        {
            var paths = Directory.GetFiles(sourceReference.ToString(), "*.mp3");
            List<ImportedSong> songs = [];
            foreach (var path in paths)
            {
                Result<Reference> reference = Reference.FromString(path);
                songs.Add(ImportFromSongReference(reference.GetValue()));
            }

            return songs;
        }

        public ImportedSong ImportFromSongReference(Reference songReference)
        {
            var file = TagLib.File.Create(songReference.ToString());
            var tags = file.Tag;
            var properties = file.Properties;

            
            var song =  new Song(
                Guid.NewGuid(),
                Name.FromString(tags.Title ?? Path.GetFileNameWithoutExtension(file.Name)).GetValue(),
                Name.FromString(tags.FirstPerformer ?? "Unknown").GetValue(),
                properties.Duration,
                false
            );

            return new ImportedSong(song, songReference);
        }
    }
}
