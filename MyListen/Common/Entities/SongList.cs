using MyArchitecture;
using MyArchitecture.DomainLayer;
using MyListen.Common.ValueObjects;

namespace MyListen.Common.Entities
{
    public class SongList(Guid id, Name name, Guid[] songs, bool isReadonly) : Entity(id)
    {
        public Name Name { get; private set; } = name;

        readonly List<Guid> songIds = [.. songs];
        public IReadOnlyList<Guid> SongIds => songIds;
        public int Count => songIds.Count;
        public bool IsReadonly { get; private set; } = isReadonly;

        public Result Rename(Name newName)
        {
            if (IsReadonly) return Result.Fail("Une songList en lecture seule ne peut être modifiée.");
            if (Name == newName) return Result.Fail("Le nouveau nom est identique");
            Name = newName;
            return Result.Ok();
        }

        public void AddNewSongsId(Guid id)
        {
            if (!songIds.Contains(id))
            {
                songIds.Add(id);
            }
        }

        public void RemoveSongId(Guid id)
        {
            songIds.Remove(id);
        }
    }
}
