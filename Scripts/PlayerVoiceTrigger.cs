using Godot;

public partial class PlayerVoiceTrigger : Area3D
{
    [Export] public int VoiceIndex = 0;
    private bool _hasPlayed = false;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (_hasPlayed) return;

        if (body.IsInGroup("Player"))
        {
            PlayerVoices.Instance.PlayVoice(VoiceIndex);
            _hasPlayed = true;
        }
    }
}