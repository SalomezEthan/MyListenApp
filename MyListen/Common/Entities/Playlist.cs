using MyArchitecture;
using MyArchitecture.DomainLayer;
using MyListen.Common.ValueObjects;

namespace MyListen.Common.Entities
{
    public class Playlist(Guid id, Name name, Guid[] childs, bool isReadonly) : Entity(id)
    {
        public Name Name { get; private set; } = name;

        readonly List<Guid> musicIds = [.. childs];
        public IReadOnlyList<Guid> MusicIds => musicIds;
        public int Count => musicIds.Count;
        public bool IsReadonly { get; private set; } = isReadonly;

        public Result Rename(Name newName)
        {
            if (IsReadonly) return Result.Fail("Une playlist en lecture seule ne peut être modifiée.");
            if (Name == newName) return Result.Fail("Le nouveau nom est identique");
            Name = newName;
            return Result.Ok();
        }

        public void AddNewMusicId(Guid id)
        {
            if (!musicIds.Contains(id))
            {
                musicIds.Add(id);
            }
        }

        public void RemoveMusicId(Guid id)
        {
            musicIds.Remove(id);
        }
    }
}
