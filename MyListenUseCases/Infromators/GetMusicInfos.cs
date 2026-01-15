using MyArchitecture;
using MyArchitecture.ApplicationLayer.UseCases.Sync;
using MyListen.Common.DataTransfertObjects;
using MyListen.Common.Entities;
using MyListen.Services.Getters.Providers;

namespace MyListenUseCases.Infromators
{
    internal record GetMusicInfosRequest(Guid MusicId);
    internal record GetMusicInfosResponse : Result
    {
        public required MusicInfos CollectedInfos { get; init; }
    }

    internal sealed class GetMusicInfos(IOutput<GetMusicInfosResponse> output, IMusicProvider provider)
    : UseCase<GetMusicInfosRequest, GetMusicInfosResponse>(output)
    {
        readonly IMusicProvider provider = provider;

        protected override GetMusicInfosResponse Do(GetMusicInfosRequest request)
        {
            Music music = provider.RetrieveByMusicid(request.MusicId);
            MusicInfos infos = MusicInfos.FromMusicEntity(music);
            return new GetMusicInfosResponse
            {
                ItWorked = true,
                Message = $"Les informations de la musique {request.MusicId} a été chargé.",
                CollectedInfos = infos
            };
        }

        protected override GetMusicInfosResponse OnError(GetMusicInfosRequest request, Exception exceptionReturned)
        {
            return new GetMusicInfosResponse
            {
                ItWorked = false,
                Message = $"Erreur lors du chargement des infos de {request.MusicId} : {exceptionReturned.Message}",
                CollectedInfos = MusicInfos.Error(request.MusicId)
            };
        }
    }
}
