using Godot;
using MacEwanGameJam26.Players;

namespace MacEwanGameJam26.Levels;

public partial class Level : Node2D
{
    [Signal]
    public delegate void OnPlayerCaughtEventHandler();

    public static Level CurrentLevel { get; private set; }

    [Export]
    public Player MainPlayer
    {
        get;
        set
        {
            if (field is not null)
                CustomLogger.LogWarning($"MainPlayer has been replaced from {field} by {value}");

            field = value;
        }
    }

    public override void _Ready()
    {
        if (CurrentLevel is not null)
            CustomLogger.LogWarning("Singleton is not null! Possibility of orphaned LevelData node.");
        CurrentLevel = this;
    }

    public override void _ExitTree()
    {
        CurrentLevel = null;
    }

    public void AnnouncePlayerCaught()
    {
        EmitSignalOnPlayerCaught();
    }
}