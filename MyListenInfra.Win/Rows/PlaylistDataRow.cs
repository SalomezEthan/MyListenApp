using LiteDB;
using MyListen.Common.Entities;

namespace MyListenInfra.Win.Rows
{
    public sealed record PlaylistDataRow
    {
        [BsonId]
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required Guid[] SongIds { get; init; }
        public required bool IsReadOnly { get; init; }

        public static PlaylistDataRow FromEntity(Playlist playlist, bool isReadOnly)
        {
            return new PlaylistDataRow
            {
                Id = playlist.Id,
                Name = playlist.Name.ToString(),
                SongIds = [.. playlist.SongIds],
                IsReadOnly = isReadOnly,
            };
        }

        public Playlist ToEntity()
        {
            return new Playlist(
                Id,
                MyListen.Common.ValueObjects.Name.FromString(Name).GetValue(),
                SongIds,
                IsReadOnly
            );
        }
    }
}
