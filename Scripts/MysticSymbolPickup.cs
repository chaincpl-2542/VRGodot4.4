using Godot;
using System;

public partial class MysticSymbolPickup : Node3D
{
	[Export] public Node RootNode;
	[Export] public Area3D area;
	[Export] public PlayParticle particle;
	[Export] public Node3D particleNode;

	public override void _Ready()
	{
		area.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}

	private void OnBodyEntered(Node3D body)
	{
		if (body.IsInGroup("PlayerHand"))
		{
			GD.Print("Picked up: Mystic Symbol");
			particleNode.Show();
			particle.PlayAllParticles();
			particle.PlayMyParticle();
			Scene5FloorController.Instance.OnFinishFloor();
			RootNode.QueueFree();
		}
	}
}
