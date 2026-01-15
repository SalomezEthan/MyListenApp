using MyListen.Common.Entities;

namespace MyListen.Common.DataTransfertObjects
{
    public sealed record MusicInfos
    {
        public required Guid Id { get; init; }
        public required string Title { get; init; }
        public required string Artist { get; init; }
        public required TimeSpan Duration { get; init; }
        public required bool IsLiked { get; init; }

        public static MusicInfos FromMusicEntity(Music music)
        {
            return new MusicInfos
            {
                Id = music.Id,
                Title = music.Title.ToString(),
                Artist = music.Artist.ToString(),
                Duration = music.Duration,
                IsLiked = music.IsFavourite,
            };
        }

        public static MusicInfos Error(Guid id)
        {
            return new MusicInfos
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
