using Godot;

namespace MacEwanGameJam26.Levels;

/// <summary>
///     This class handles the transition between the exterior and interior sprites of buildings when the player enters
///     them.
/// </summary>
public partial class Room : Node2D
{
    [Export] private Node2D _exteriorNode;
    [Export] private Node2D _interiorNode;

    [Export] private float _opacityTransitionTimeSeconds = 1f;
    [Export] private Area2D _playerInteriorArea;

    private void OnPlayerExitBuilding(Node2D body)
    {
        CustomLogger.LogDebug("Exit");
        /*_exteriorNode?.Show();
        _interiorNode?.Hide();*/
        AnimateOpacity(_exteriorNode, false);
    }

    private void OnPlayerEnterBuilding(Node2D body)
    {
        CustomLogger.LogDebug("Enter");
        /*_exteriorNode?.Hide();
        _interiorNode?.Show();*/
        AnimateOpacity(_exteriorNode, true);
    }

    private void AnimateOpacity(Node2D node, bool toTransparent)
    {
        var tween = CreateTween();
        if (toTransparent)
            tween.TweenProperty(node, "modulate", Colors.Transparent, _opacityTransitionTimeSeconds);
        else
            tween.TweenProperty(node, "modulate", Colors.White, _opacityTransitionTimeSeconds);
    }
}