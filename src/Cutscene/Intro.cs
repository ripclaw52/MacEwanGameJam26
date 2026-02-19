using Godot;
using System;

public partial class Intro : CanvasLayer
{
    private AnimationPlayer _animPlayer;
    private TextureRect _imageDisplay;
    [Export] private string _nextScenePath = "res://scenes/MasterScene.tscn";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
        _animPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _imageDisplay = GetNode<TextureRect>("IntroCutsceneText");

        _animPlayer.AnimationFinished += OnAnimationFinished;
        _animPlayer.Play("CutsceneSequence");
    }

    private void OnAnimationFinished(StringName animName)
    {
        if (animName == "CutsceneSequence")
        {
            // Transition to main game
            GetTree().ChangeSceneToFile(_nextScenePath);
        }
    }
}
