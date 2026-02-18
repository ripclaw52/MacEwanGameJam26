using Godot;

namespace MacEwanGameJam26.Players;

/// <summary>
///     This class handles the shape-shifting mechanic. It stores the current shape as well.
/// </summary>
/// <remarks>
///     Despite being called controller, it is not responsible for changing the ShiftForm.
///     It is up to the PlayerShiftForm to handle how switching to it works.
/// </remarks>
public partial class PlayerShapeShiftingController : Node
{
    /// <summary>
    ///     This signal is fired when the CurrentForm property is modified.
    /// </summary>
    [Signal]
    public delegate void OnFormChangedEventHandler(PlayerShiftForm old, PlayerShiftForm new0);

    /// <remarks>
    ///     OnFormChanged signal will NOT be fired when shifting to the same form
    /// </remarks>
    [Export]
    public PlayerShiftForm CurrentForm
    {
        get;
        set
        {
            if (value == field)
                return;

            var old = field;
            field = value;
            EmitSignalOnFormChanged(old, value);
            CustomLogger.LogDebug($"Shifted to {value.FormName}");
        }
    }

    public override void _Ready()
    {
        if (CurrentForm is not null)
            EmitSignalOnFormChanged(CurrentForm, CurrentForm);

        foreach (var node in GetChildren())
        {
            if (node is not PlayerShiftForm form)
                continue;
            CustomLogger.LogDebug($"Registering form {form.GetType().Name}");
        }
    }
}