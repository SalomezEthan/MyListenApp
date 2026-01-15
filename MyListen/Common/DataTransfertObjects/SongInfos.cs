using MyListen.Common.Entities;

namespace MyListen.Common.DataTransfertObjects
{
    public sealed record SongInfos
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required string Artist { get; init; }
        public required TimeSpan Duration { get; init; }
        public required bool IsLiked { get; init; }

        public static SongInfos FromSongEntity(Entities.Song song)
        {
            return new SongInfos
            {
                Id = song.Id,
                Title = song.Title.ToString(),
                Artist = song.Artist.ToString(),
                Duration = song.Duration,
                IsLiked = song.IsFavourite,
            };
        }

        public static SongInfos Error(Guid id)
        {
            return new SongInfos
            {
                Id = id,
                Title = string.Empty,
                Artist = string.Empty,
                Duration = TimeSpan.Zero,
                IsLiked = false,
            };
        }
    }
}
