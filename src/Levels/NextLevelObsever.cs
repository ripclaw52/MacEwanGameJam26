using Godot;
using MacEwanGameJam26.Players;
using MacEwanGameJam26.UI;

public partial class NextLevelObsever : Node2D
{
    // HACK: CaughtScreen is also used as the completion scene
    [Export] private CaughtScreen _completionScene;
    [Export] private Area2D _detectionArea;
    [Export] private Player _player;
    private bool _valuableCollected;

    public override void _Process(double delta)
    {
        // PERFORMANCE HIT
        if (!_detectionArea.OverlapsBody(_player))
            return;
        if (!_valuableCollected)
            return;

        _completionScene.HandlePlayerCompletedLevel();
    }

    private void HandleValuablePickedUp()
    {
        _valuableCollected = true;
    }
}