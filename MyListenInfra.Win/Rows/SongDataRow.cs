using LiteDB;
using MyListen.Common.Entities;
using MyListen.Common.ValueObjects;

namespace MyListenInfra.Win.Rows
{
    public sealed record SongDataRow
    {
        [BsonId]
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required string Artist { get; init; }
        public required TimeSpan Duration { get; init; }
        public required bool IsLiked { get; init; }
        public required string Reference { get; init; }

        public static SongDataRow FromEntity(Song song, string reference)
        {
            return new SongDataRow
            {
                Id = song.Id,
                Title = song.Title.ToString(),
                Artist = song.Artist.ToString(),
                Duration = song.Duration,
                IsLiked = song.IsFavourite,
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
