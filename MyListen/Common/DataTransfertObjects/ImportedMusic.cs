using MyListen.Common.Entities;
using MyListen.Common.ValueObjects;
namespace MyListen.Common.DataTransfertObjects
{
    public sealed record ImportedMusic(Music Entity, Reference MusicReference);
}
