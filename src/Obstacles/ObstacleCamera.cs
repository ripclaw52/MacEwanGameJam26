using System;
using System.Collections.Generic;
using Godot;
using MacEwanGameJam26.Levels;
using MacEwanGameJam26.Players;

namespace MacEwanGameJam26.Obstacles;

public partial class ObstacleCamera : Node2D
{
    [Export] private ProgressBar _bar;

    [Export] private Area2D _detectionArea;
    private float _detectionTime;
    private bool _hasPlayerBeenDetected;
    private bool _isPlayerBeingDetected;

    [Export] private float _timeToDetectionSec = 0.4f;

    private PlayerShapeShiftingController ShapeShiftingController =>
        Level.CurrentLevel.MainPlayer.ShapeShiftingController;

    public override void _Ready()
    {
        if (_detectionArea is null)
        {
            CustomLogger.LogError("_detectionArea is null");
            return;
        }

        _detectionArea.BodyEntered += OnAreaEntered;
        _detectionArea.BodyExited += OnAreaExited;
    }

    public override void _Process(double delta)
    {
        if (_hasPlayerBeenDetected)
            return;

        if (_isPlayerBeingDetected && ShapeShiftingController.CurrentForm is not PlayerShiftFormCat)
        {
            _detectionTime += (float)GetProcessDeltaTime();
            if (_detectionTime >= _timeToDetectionSec)
                DetectPlayer();
        }
        else
        {
            // Decay detection time
            _detectionTime -= (float)GetProcessDeltaTime() * 0.5f;
        }

        _detectionTime = Math.Clamp(_detectionTime, 0, _timeToDetectionSec);
        _bar.Value = _detectionTime / _timeToDetectionSec;
        if (_bar.Value == 0)
            _bar.Hide();
        else
            _bar.Show();
    }

    public override string[] _GetConfigurationWarnings()
    {
        var errs = new List<string>();
        if (_detectionArea is null)
            errs.Add("_detectionArea is null");

        return [.. errs];
    }

    private void DetectPlayer()
    {
        _hasPlayerBeenDetected = true;
        Level.CurrentLevel.AnnouncePlayerCaught();
        CustomLogger.LogDebug($"Player detected by {Name} ({GetType().Name})");
    }

    private void OnAreaEntered(Node2D node)
    {
        if (node is not Player)
            return;

        _isPlayerBeingDetected = true;
    }

    private void OnAreaExited(Node2D node)
    {
        if (node is not Player)
            return;
        _isPlayerBeingDetected = false;
    }
}