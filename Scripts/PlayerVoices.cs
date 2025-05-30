using Godot;
using System;
using System.Collections.Generic;

public partial class PlayerVoices : Node3D
{
    public static PlayerVoices Instance { get; private set; }

    [Export] public AudioStreamPlayer3D VoicePlayer;
    [Export] public Godot.Collections.Array<AudioStream> VoiceClips = new();
    private HashSet<int> playedIndexes = new();

    public override void _Ready()
    {
        Instance = this;
    }

    public void PlayVoice(int index)
    {
        if (index < 0 || index >= VoiceClips.Count)
        {
            GD.PrintErr($"Voice index {index} is out of range.");
            return;
        }

        if (playedIndexes.Contains(index))
        {
            GD.Print($"Voice {index} already played.");
            return;
        }

        VoicePlayer.Stream = VoiceClips[index];
        VoicePlayer.Play();
        playedIndexes.Add(index);
    }
}