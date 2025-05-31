using Godot;
using System;

public partial class PlayOnceAudioArea : Area3D
{
    [Export] public string TargetGroup = "Player";
    private bool _hasPlayed = false;
    private AudioStreamPlayer3D _audioPlayer;

    public override void _Ready()
    {
        _audioPlayer = GetNode<AudioStreamPlayer3D>("AudioStreamPlayer3D");
        this.BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (_hasPlayed) return;

        if (body.IsInGroup(TargetGroup))
        {
            GD.Print("Player entered. Playing audio...");
            _audioPlayer.Play();
            _hasPlayed = true;
        }
    }
}