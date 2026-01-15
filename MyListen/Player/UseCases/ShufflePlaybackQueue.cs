using MyArchitecture;
using MyArchitecture.ApplicationLayer;
using MyListen.Common.Services;

namespace MyListen.Player.UseCases
{
    public sealed record ShufflePlaybackQueueRequest(Guid StartSongId);

    public sealed class ShufflePlaybackQueue(PlaybackQueue playbackQueue)
    : UseCase<ShufflePlaybackQueueRequest>
    {
        readonly PlaybackQueue playbackQueue = playbackQueue;

        public override void Execute(ShufflePlaybackQueueRequest request)
        {
            Result result = playbackQueue.ShufflePlayBackQueue(request.StartSongId);
            if (!result.IsSuccess) Send(Result.Fail($"Impossible de mélanger la file de lecture : {result.GetFailure().BrokenRule}"));
            else Send(Result.Ok());
        }
    }
}
