using Godot;

namespace MacEwanGameJam26.Players;

/// <summary>
///     This class is responsible for handling the particle effects when the player changes forms
/// </summary>
public partial class OnFormShiftFeedbackParticles : CpuParticles2D
{
    [Export] private PlayerShapeShiftingController _shiftingController;

    public override void _Ready()
    {
        if (_shiftingController is null)
        {
            CustomLogger.LogWarning("_shiftingController is null. Shifting particle effects disabled.");
            return;
        }

        _shiftingController?.OnFormChanged += (_, _) =>
        {
            Emitting = false;
            Callable.From(() => { Emitting = true; }).CallDeferred();
        };
    }
}