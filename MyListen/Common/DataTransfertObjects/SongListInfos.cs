using MyListen.Common.Entities;

namespace MyListen.Common.DataTransfertObjects
{
    public sealed record SongListInfos
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required int Count { get; init; }

        public static SongListInfos FromEntity(Entities.SongList songList)
        {
            return new SongListInfos
            {
                Id = songList.Id,
                Name = songList.Name.ToString(),
                Count = songList.Count
            };
        }
    }
}
