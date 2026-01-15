using MyArchitecture;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Common.Services.Importers;
using MyListen.Common.ValueObjects;

namespace MyListenInfra.Win.Importers
{
    internal sealed class TagLibMusicImporter : IMusicImporter
    {
        public IReadOnlyList<ImportedMusic> ImportMusicsFromSource(Reference sourceReference)
        {
            var paths = Directory.GetFiles(sourceReference.ToString(), "*.mp3");
            List<ImportedMusic> musics = [];
            foreach (var path in paths)
            {
                Result<Reference> reference = Reference.FromString(path);
                musics.Add(ImportFromMusicReference(reference.GetValue()));
            }

            return musics;
        }

        public ImportedMusic ImportFromMusicReference(Reference musicReference)
        {
            var file = TagLib.File.Create(musicReference.ToString());
            var tags = file.Tag;
            var properties = file.Properties;

            
            var music =  new Song(
                Guid.NewGuid(),
                Name.FromString(tags.Title ?? Path.GetFileNameWithoutExtension(file.Name)).GetValue(),
                Name.FromString(tags.FirstPerformer ?? "Unknown").GetValue(),
                properties.Duration,
                false
            );

            return new ImportedMusic(music, musicReference);
        }
    }
}
