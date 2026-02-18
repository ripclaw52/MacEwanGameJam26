using Godot;

namespace MacEwanGameJam26.Players;

/// <summary>
///     This is the base class of forms the Player can shift into.
/// </summary>
public partial class PlayerShiftForm : Node
{
    [Export] protected PlayerShapeShiftingController ShiftingController;

    [Export] public string FormName { get; private set; }

    /// <summary>
    ///     This is the name of the Action shortcut that triggers the form shifting.
    /// </summary>
    [Export]
    public StringName ActionShortcutName { get; private set; }

    protected void Shift(PlayerShiftForm newForm)
    {
        ShiftingController.CurrentForm = newForm;
    }
}