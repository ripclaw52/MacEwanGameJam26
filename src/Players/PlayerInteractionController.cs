using System.Linq;
using Godot;
using MacEwanGameJam26.Interfaces;

namespace MacEwanGameJam26.Players;

/// <summary>
///     This is the class responsible for handling interacting with interactables.
/// </summary>
public partial class PlayerInteractionController : Area2D
{
    /// <summary>
    ///     Has something been interacted with in one hold of the "interaction" action
    /// </summary>
    private bool _hasInteracted;

    /// <summary>
    ///     How long to hold to count as interact
    /// </summary>
    [Export] private float _holdTimeUntilInteract = 0.5f;

    /// <summary>
    ///     How long the interact button has been held
    /// </summary>
    private float _interactHoldDuration;

    [Export] private Player _player;

    // NOTE (Erwin): 
    // As of f63d39, PlayerInteractionController is set to only detect Area2Ds with its 
    // Collision Layer set to "Interactable"
    public override void _Process(double delta)
    {
        // Only proceed when there is another Area2D within this controller's CollisionShape2D
        if (GetOverlappingAreas().Count == 0)
        {
            _interactHoldDuration = 0;
            _hasInteracted = false;
            return;
        }

        if (Input.IsActionPressed("Interact"))
        {
            _interactHoldDuration += (float)delta;

            if (_interactHoldDuration >= _holdTimeUntilInteract && !_hasInteracted)
            {
                Interact();
                _hasInteracted = true;
            }

            if (!_hasInteracted)
                CustomLogger.LogDebug(_interactHoldDuration);
        }
        else
        {
            _hasInteracted = false;
            _interactHoldDuration = 0;
        }
    }

    // NOTE (Erwin):
    // Don't know how this will handle interactions with overlapping interactables. 
    private void Interact()
    {
        var area = GetOverlappingAreas().FirstOrDefault();
        if (area is not IInteractable interactable)
            return;

        interactable.Interact(_player);
    }

    private void OnArea2DEntered(Area2D area)
    {
        // STUB
    }
}