using LiteDB;
using MyListen.Common.Entities;
using MyListen.Common.ValueObjects;

namespace MyListenInfra.Win.Rows
{
    public sealed record MusicDataRow
    {
        [BsonId]
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required string Artist { get; init; }
        public required TimeSpan Duration { get; init; }
        public required bool IsLiked { get; init; }
        public required string Reference { get; init; }

        public static MusicDataRow FromEntity(Song music, string reference)
        {
            return new MusicDataRow
            {
                Id = music.Id,
                Title = music.Title.ToString(),
                Artist = music.Artist.ToString(),
                Duration = music.Duration,
                IsLiked = music.IsFavourite,
                Reference = reference
            };
        }

        public Song ToEntity()
        {
            return new Song(
                Id,
                Name.FromString(Title).GetValue(),
                Name.FromString(Artist).GetValue(),
                Duration,
                IsLiked
            );
        }
    }
}
