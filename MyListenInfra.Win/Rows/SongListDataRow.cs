using LiteDB;
using MyListen.Common.Entities;

namespace MyListenInfra.Win.Rows
{
    public sealed record SongListDataRow
    {
        [BsonId]
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required Guid[] SongIds { get; init; }
        public required bool IsReadOnly { get; init; }

        public static SongListDataRow FromEntity(SongList songList, bool isReadOnly)
        {
            return new SongListDataRow
            {
                Id = songList.Id,
                Name = songList.Name.ToString(),
                SongIds = [.. songList.SongIds],
                IsReadOnly = isReadOnly,
            };
        }

        public SongList ToEntity()
        {
            return new SongList(
                Id,
                MyListen.Common.ValueObjects.Name.FromString(Name).GetValue(),
                SongIds,
                IsReadOnly
            );
        }
    }
}
