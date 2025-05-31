using Godot;
using System;

public partial class BookPickup : Node3D
{
	[Export] public Node3D Model;
	[Export] public Area3D area;
	[Export] public PlayParticle particle;
	[Export] public Node3D particleNode;
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
				GD.Print("Picked up: Book!");
				particleNode.Show();
				particle.PlayAllParticles();
				particle.PlayMyParticle();
				Model.Hide();
				Scene4FloorController.Instance.OnFinishFloor();
				isTriggered = true;
			}
		}
	}
}
