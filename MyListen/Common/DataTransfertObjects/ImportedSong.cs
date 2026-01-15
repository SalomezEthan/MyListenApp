using MyListen.Common.Entities;
using MyListen.Common.ValueObjects;
namespace MyListen.Common.DataTransfertObjects
{
    public sealed record ImportedSong(Entities.Song Entity, Reference SongReference);
}
