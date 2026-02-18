using System.Collections.Generic;
using Godot;

namespace MacEwanGameJam26.Levels;

/// <summary>
///     This class is responsible for storing the valuables the Player has stolen, and storing the total amount of
///     money the stolen valuables are worth
/// </summary>
public partial class LevelMoneyController : Node
{
    // NOTE (Erwin): Currently unused
    private readonly List<(string, int)> _valuablesStolen = [];
    [Export] private Label _moneyStolenLabel;

    public int MoneyStolen
    {
        get;
        set
        {
            field = value;
            _moneyStolenLabel.Text = $"Money Stolen: ${value}";
        }
    } = 0;
}