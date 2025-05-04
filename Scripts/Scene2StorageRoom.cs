using Godot;
using System;

public partial class Scene2StorageRoom : BaseFloorController
{
	[Export] public Area3D _area;
	[Export] public Node3D ghostEvent;


	private bool _triggered = false;

	public override void _Ready()
	{
		

		if (_area != null)
		{
			_area.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
			GD.Print("Area3D assigned!");
		}
		else
		{
			GD.PrintErr("Area3D not assigned!");
		}
	}

	private void OnBodyEntered(Node3D body)
	{
		if (_triggered) return;

		if (body.IsInGroup("Player"))
		{
			_triggered = true;

			GD.Print("✅ Player entered the Storage room.");

			// Disable storage room trigger
			_area.Visible = false;
			_area.Monitoring = false;

			// Hide self (optional)
			this.Visible = false;

			// Enable ghost event trigger
			if (ghostEvent != null)
			{
				ghostEvent.Visible = true;
				var ghostArea = ghostEvent.GetNodeOrNull<Area3D>("Area3D");
				if (ghostArea != null)
					ghostArea.Monitoring = true;
			}
		}
	}
}
