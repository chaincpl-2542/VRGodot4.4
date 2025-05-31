using Godot;
using System;

public partial class MysticSymbolInteraction : Node
{
	[Export] public Node3D symbolNode3D;
	[Export] public Area3D area;
	[Export] public AnimationPlayer animationPlayer;
	[Export] public PlayParticle particle;
	[Export] public Node3D particleNode;
	[Export] public AudioStreamPlayer3D _audioPlayer1;
	[Export] public AudioStreamPlayer3D _audioPlayer2;
	[Export] public AudioStreamPlayer3D _audioPlayer3;
	
	bool isTriggered = false;
	public override void _Ready()
	{
		area.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}

	private void OnBodyEntered(Node3D body)
	{
		if (body.IsInGroup("PlayerHand"))
		{
			if (isTriggered == false)
			{
				_audioPlayer1.Play();
				_audioPlayer2.Play();
				_audioPlayer3.Play();
				
				GD.Print("Place : MysticSymbol");
				particleNode.Show();
				particle.PlayAllParticles();
				particle.PlayMyParticle();
				Scene6FloorController.Instance.OnFinishFloor();
				symbolNode3D.Visible = true;
				animationPlayer.Play("DoorClose");
				isTriggered = true;
			}
		}
	}
}
