using Godot;
using System;

public partial class BookShelfInteraction : Node3D
{
	[Export] public Node3D BookNode;
	[Export] public Area3D area;
	[Export] public AnimationPlayer _animPlayer;
	[Export] public AudioStreamPlayer3D InsertSound;
	[Export] public PlayParticle particle;
	[Export] public Node3D particleNode;

	private bool _triggered = false;

	public override void _Ready()
	{
		if (area != null)
			area.BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node3D body)
	{
		if (_triggered)
			return;

		if (body.IsInGroup("PlayerHand"))
		{
			particleNode.Show();
			particle.PlayMyParticle();
			GD.Print("📚 Book inserted into shelf.");
			BookNode.Visible = true;
			InsertSound?.Play();
			_animPlayer?.Play("BookShelfInteraction");
			_triggered = true;
		}
	}
}
