using Godot;

namespace MacEwanGameJam26.Levels;

/// <summary>
///     This class handles the transition between the exterior and interior sprites of buildings when the player enters
///     them.
/// </summary>
public partial class Building : Node2D
{
    [Export] private Sprite2D _exteriorSprite;
    [Export] private Sprite2D _interiorSprite;
    [Export] private Area2D _playerInteriorArea;

    private void OnPlayerExitBuilding(Node2D body)
    {
        CustomLogger.LogDebug("Exit");
        _exteriorSprite.Show();
        _interiorSprite.Hide();
    }

    private void OnPlayerEnterBuilding(Node2D body)
    {
        CustomLogger.LogDebug("Enter");
        _exteriorSprite.Hide();
        _interiorSprite.Show();
    }
}