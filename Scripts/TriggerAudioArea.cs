using Godot;

public partial class TriggerAudioArea : Area3D
{
    [Export] public AudioStreamPlayer3D Audio;
    [Export] public Node3D DialogText;
    private bool isPlay = false;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node3D body)
    {
        if (body.IsInGroup("Player"))
        {
            if (!isPlay)
            {
                if (Audio != null && !Audio.Playing)
                {
                    Audio.Play();
                    DialogText.Visible = true;
                    isPlay = true;
                }
            }
        }
    }
}