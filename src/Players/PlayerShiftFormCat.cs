using Godot;

namespace MacEwanGameJam26.Players;

public partial class PlayerShiftFormCat : PlayerShiftForm
{
    [Export] private float _timeLimit = 3f;

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(ActionShortcutName))
            Shift(this);
    }
}