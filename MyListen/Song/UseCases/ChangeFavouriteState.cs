using MyArchitecture.ApplicationLayer;
using MyListen.Common.Entities;
using MyListen.Common.Services.Stores;

namespace MyListen.Song.UseCases
{
    public sealed record ChangeFavouriteStateRequest(Guid MusicId, bool IsFavourite);

    public sealed class ChangeFavouriteState(IMusicStore musicStore)
    : NoResponseUseCase<ChangeFavouriteStateRequest>
    {
        readonly IMusicStore musicStore = musicStore;

        public override void Execute(ChangeFavouriteStateRequest request)
        {
            Music music = musicStore.GetByMusicId(request.MusicId);
            if (!request.IsFavourite) music.Like();
            else music.Dislike();
            musicStore.UpdateMusic(music);
        }
    }

    public enum NextFavouriteState
    {
        Favourite,
        NotFavourite
    }
}
