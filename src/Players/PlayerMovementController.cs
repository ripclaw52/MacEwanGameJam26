using System;
using Godot;

namespace MacEwanGameJam26.Players;

/// <summary>
///     This class handles the player (CharacterBody2D) movement.
/// </summary>
public partial class PlayerMovementController : Node
{
    /// <summary>
    ///     Magnitude of acceleration due to gravity in Pixels/Second.
    /// </summary>
    /// <remarks>
    ///     The "100" constant is an arbitrary number to make the player fall faster
    /// </remarks>
    private float _gravityAcceleration = 9.81f * 100;

    /// <summary>
    ///     How fast the player moves in Pixels/Second
    /// </summary>
    [Export] private float _moveSpeed = 1600f;

    [Export] private Player _player;

    public override void _Process(double delta)
    {
        CalculateVelocity();
        ApplyGravity();

        // MoveAndSlide is the method used by CharacterBody2Ds to move the character using its Velocity
        _player.MoveAndSlide();
    }

    private void ApplyGravity()
    {
        var downVec = _gravityAcceleration;
        _player.Velocity = new Vector2(_player.Velocity.X, downVec);
    }

    private void CalculateVelocity()
    {
        var left = Input.IsActionPressed("MoveLeft") ? 1 : 0;
        var right = Input.IsActionPressed("MoveRight") ? 1 : 0;

        float inputX = right - left;

        var moveLength = (float)Math.Sqrt(inputX * inputX);

        if (moveLength > 0)
            inputX /= moveLength;

        var move = new Vector2(
            inputX * _moveSpeed,
            0
        );

        _player.Velocity = move;
    }
}