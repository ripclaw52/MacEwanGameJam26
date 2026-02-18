using Godot;
using System;

public partial class PauseMenu : Control
{
	AnimationPlayer animationPlayer;
    public override void _Ready()
    {
		animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("RESET");
		Hide();
    }

    public override void _Process(double delta)
    {
		TestEsc();
    }

    public void Resume()
	{
		GetTree().Paused = false;
		animationPlayer.PlayBackwards("blur");
		Hide();
	}

	public void Pause()
	{
		Show();
		GetTree().Paused = true;
		animationPlayer.Play("blur");
	}

	public void TestEsc()
	{
		if (Input.IsActionJustPressed("Escape") && GetTree().Paused == false)
		{
			Pause();
		}
		else if (Input.IsActionJustPressed("Escape") && GetTree().Paused == true)
		{
			Resume();
		}

    }

    public void OnResumePressed()
	{
		Resume();
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
}
