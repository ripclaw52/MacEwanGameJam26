using Godot;
using System;

public partial class MainMenu : Control
{
	int MasterBusIndex = AudioServer.GetBusIndex("Master");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnPlayPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/MasterScene.tscn");
	}

	public void OnQuitPressed()
	{
		GetTree().Quit();
	}

	public void OnMasterVolumeSliderValueChanged(float value)
	{
		AudioServer.SetBusVolumeDb(MasterBusIndex, Mathf.LinearToDb(value));
	}
}
