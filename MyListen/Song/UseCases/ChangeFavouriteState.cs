using MyArchitecture.ApplicationLayer;
using MyListen.Common.Entities;
using MyListen.Common.Services.Stores;

namespace MyListen.Song.UseCases
{
    public sealed record ChangeFavouriteStateRequest(Guid SongId, bool IsFavourite);

    public sealed class ChangeFavouriteState(ISongRespository songStore)
    : NoResponseUseCase<ChangeFavouriteStateRequest>
    {
        readonly ISongRespository songStore = songStore;

        public override void Execute(ChangeFavouriteStateRequest request)
        {
            Common.Entities.Song song = songStore.GetBySongId(request.SongId);
            if (!request.IsFavourite) song.Like();
            else song.Dislike();
            songStore.UpdateSong(song);
        }
    }
}
