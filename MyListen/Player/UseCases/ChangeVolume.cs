using MyArchitecture;
using MyArchitecture.ApplicationLayer;
using MyListen.Common.Services;
using MyListen.Common.ValueObjects;

namespace MyListen.Player.UseCases;

public sealed record ChangeVolumeRequest(float volume);

public sealed class ChangeVolume(ISongPlayer volumeModifier)
: UseCase<ChangeVolumeRequest>
{
    readonly ISongPlayer volumeModifier = volumeModifier;

    public override void Execute(ChangeVolumeRequest request)
    {
        Result<Volume> volume = Volume.FromFloat(request.volume);
        if (!volume.IsSuccess)
        {
            Send(Result.Fail($"La valeur utilisée pour le volume est incorrecte : {volume.GetFailure().BrokenRule}"));
            return;
        }

        volumeModifier.ChangeVolume(volume.GetValue());
        Send(Result.Ok());
    }
}
