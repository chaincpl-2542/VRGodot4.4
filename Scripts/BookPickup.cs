using Godot;
using System;

public partial class BookPickup : Node3D
{
	[Export] public Node RootNode;
	[Export] public Area3D area;

	public override void _Ready()
	{
		area.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}

	private void OnBodyEntered(Node3D body)
	{
		if (body.IsInGroup("PlayerHand"))
		{
			GD.Print("Picked up: " + RootNode.Name);
			Scene4FloorController.Instance.OnFinishFloor();
			RootNode.QueueFree();
		}
	}
}
