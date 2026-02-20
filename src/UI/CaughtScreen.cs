using Godot;

namespace MacEwanGameJam26.UI;

public partial class CaughtScreen : CanvasLayer
{
    [Export] private AnimationPlayer _animationPlayer;

    public void HandlePlayerCaught()
    {
        Show();
        _animationPlayer.Play("blur");
        GetTree().Paused = true;
    }

    public void OnRestartPressed()
    {
        Resume();
        GetTree().ReloadCurrentScene();
    }

    public void OnBackToMenuPressed()
    {
        Resume();
        GetTree().ChangeSceneToFile("res://scenes/UI/MainMenu.tscn");
    }

    private void Resume()
    {
        GetTree().Paused = false;
        _animationPlayer.PlayBackwards("blur");
        Hide();
    }
}