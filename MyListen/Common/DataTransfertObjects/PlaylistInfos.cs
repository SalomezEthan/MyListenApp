namespace MyListen.Common.DataTransfertObjects
{
    public sealed record PlaylistInfos
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required int Count { get; init; }
    }
}
