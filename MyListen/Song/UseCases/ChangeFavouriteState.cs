using MyArchitecture.ApplicationLayer;
using MyListen.Common.Entities;
using MyListen.Common.Services.Stores;

namespace MyListen.Song.UseCases
{
    public sealed record ChangeFavouriteStateRequest(Guid SongId, bool IsFavourite);

    public sealed class ChangeFavouriteState(ISongRespository songRepo)
    : NoResponseUseCase<ChangeFavouriteStateRequest>
    {
        readonly ISongRespository songRepo = songRepo;

        public override void Execute(ChangeFavouriteStateRequest request)
        {
            Common.Entities.Song song = songRepo.GetBySongId(request.SongId);
            if (!request.IsFavourite) song.Like();
            else song.Dislike();
            songRepo.UpdateSong(song);
        }
    }
}
