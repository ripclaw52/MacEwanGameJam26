using Godot;
using MacEwanGameJam26.Levels;
using MacEwanGameJam26.Players;

namespace MacEwanGameJam26.Interactables;

public partial class InteractableValuable : BaseInteractable
{
    [Signal]
    public delegate void OnValuablePickedUpEventHandler();

    private bool _hasBeenInteractedWith;
    [Export] private Node2D _interactableCircle;
    [Export] private Sprite2D _interactableInnerCircle;

    private bool _isMouseInsideArea;
    [Export] private LevelMoneyController _moneyController;
    [Export] private Label _onStealLabel;
    [Export] private Player _player;
    [Export] public int MonetaryValue = 10;
    [Export] public int PickUpRange = 300;

    public override void _Ready()
    {
        MouseEntered += () =>
        {
            _isMouseInsideArea = true;
            if (GlobalPosition.DistanceTo(_player.GlobalPosition) <= PickUpRange)
                ShowInteractableCircle();
        };
        MouseExited += () =>
        {
            _isMouseInsideArea = false;
            HideInteractableCircle();
        };
    }

    public override void _Process(double delta)
    {
        // Only allow interaction when Player is near and if mouse is inside this object's area
        if (Input.IsMouseButtonPressed(MouseButton.Left) && _isMouseInsideArea &&
            GlobalPosition.DistanceTo(_player.GlobalPosition) <= PickUpRange)
        {
            _interactableInnerCircle.Scale += Vector2.One * (float)delta;
            _interactableInnerCircle.Scale = _interactableInnerCircle.Scale.Clamp(Vector2.Zero, Vector2.One);

            if (_interactableInnerCircle.Scale == Vector2.One)
                Interact();
        }
        else
        {
            _interactableInnerCircle.Scale -= Vector2.One * (float)delta;
            _interactableInnerCircle.Scale = _interactableInnerCircle.Scale.Clamp(Vector2.Zero, Vector2.One);
        }
    }

    private void ShowInteractableCircle()
    {
        var tween = CreateTween().SetTrans(Tween.TransitionType.Expo);
        tween.TweenProperty(_interactableCircle, "modulate", Colors.White, 0.5f);
    }

    private void HideInteractableCircle()
    {
        var tween = CreateTween().SetTrans(Tween.TransitionType.Expo).SetParallel();
        tween.TweenProperty(_interactableCircle, "modulate", Colors.Transparent, 0.5f);
        tween.TweenProperty(_interactableInnerCircle, "scale", Vector2.Zero, 0.5f);
    }

    public override void Interact()
    {
        if (_hasBeenInteractedWith)
            return;

        _hasBeenInteractedWith = true;
        CustomLogger.LogDebug($"Got {Name}");
        _moneyController.MoneyStolen += MonetaryValue;

        EmitSignalOnValuablePickedUp();

        _onStealLabel.Show();

        // Show the "+$XX" text.
        var tween = CreateTween().SetEase(Tween.EaseType.Out);
        tween.Parallel().SetTrans(Tween.TransitionType.Expo).TweenProperty(_onStealLabel,
            "position",
            _onStealLabel.Position + Vector2.Up * 20,
            1f);
        tween.Parallel().TweenProperty(this, "modulate", Colors.Transparent, 2f);
        tween.TweenCallback(Callable.From(QueueFree));
    }
}