using Godot;
using System;

public partial class Scene2GhostEventTrigger : BaseFloorController
{
	[Export] public Area3D _area;
	[Export] public Node3D ghostNode3D;
	[Export] public Node3D floorCheck;

	[Export] public double MaxTimer = 10.0;

	[Export] public AudioStreamPlayer GhostAppearSound;
	[Export] public AudioStreamPlayer GhostVanishSound;

	[Export] public Godot.Collections.Array<OmniLight3D> FlickerLights = new();

	private bool _triggered = false;
	private double _timer = 0;

	// Track original colors so we can restore them
	private Godot.Collections.Dictionary<OmniLight3D, Color> _originalLightColors = new();

	public override void _Ready()
	{
		ghostNode3D.Visible = false;
		_area.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
		SetProcess(true);

		// Store original light colors
		foreach (var light in FlickerLights)
		{
			_originalLightColors[light] = light.LightColor;
		}
	}

	public override void _Process(double delta)
	{
		if (_triggered)
		{
			_timer += delta;

			if (_timer >= MaxTimer)
			{
				ghostNode3D.Visible = false;

				if (GhostVanishSound != null)
					GhostVanishSound.Play();

				// Reset lights to original state
				foreach (var light in FlickerLights)
				{
					light.Visible = true;
					if (_originalLightColors.ContainsKey(light))
						light.LightColor = _originalLightColors[light];
				}

				_triggered = false;
				SetProcess(false);
			}
		}
	}

	private void OnBodyEntered(Node3D body)
	{
		if (body.IsInGroup("Player") && !_triggered)
		{
			ghostNode3D.Visible = true;

			if (GhostAppearSound != null)
				GhostAppearSound.Play();

			_area.Visible = false;
			_area.Monitoring = false;

			floorCheck.Visible = true;
			floorCheck.GetNode<Area3D>("Area3D").Monitoring = true;

			_triggered = true;
			_timer = 0;

			FlickerLightsHard();

			GD.Print("Player triggered ghost event.");
		}
	}

	private async void FlickerLightsHard()
	{
		foreach (var light in FlickerLights)
		{
			light.LightColor = new Color(1, 0, 0); // Bright red
		}

		for (int i = 0; i < 10; i++)
		{
			foreach (var light in FlickerLights)
			{
				light.Visible = !light.Visible;
			}
			await ToSignal(GetTree().CreateTimer(0.05f + (float)GD.RandRange(0.01, 0.03)), "timeout");
		}

		// Lights stay ON after flicker
		foreach (var light in FlickerLights)
		{
			light.Visible = true;
		}
	}
}
