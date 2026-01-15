using MyArchitecture;
using MyArchitecture.DomainLayer;
using MyListen.Common.ValueObjects;

namespace MyListen.Common.Entities
{
    public class Music : Entity
    {
        public Name Title { get; private set; }
        public Name Artist { get; private set; }
        public TimeSpan Duration { get; private set; }
        public bool IsFavourite { get; private set; }

        public Music(Guid id, Name title, Name artist, TimeSpan duration, bool isLiked)
        : base(id)
        {

            Id = id;
            Title = title;
            Artist = artist;
            Duration = duration;
            IsFavourite = isLiked;
        }

        public Result Rename(Name newTitle)
        {
            if (Title == newTitle) return Result.Fail("Le titre est identique");
            Title = newTitle;
            return Result.Ok();
        }

        public Result ChangeArtist(Name newArtist)
        {
            if (Artist == newArtist) return Result.Fail("L'artiste est identique");
            Artist = newArtist;
            return Result.Ok();
        }

        public void Like() => IsFavourite = true;
        public void Dislike() => IsFavourite = false;
    }
}
