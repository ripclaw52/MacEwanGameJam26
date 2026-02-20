using Godot;

namespace MacEwanGameJam26.Players;

public partial class Player : CharacterBody2D
{
    [Export] public PlayerMovementController MovementController { get; private set; }
    [Export] public PlayerShapeShiftingController ShapeShiftingController { get; private set; }
}