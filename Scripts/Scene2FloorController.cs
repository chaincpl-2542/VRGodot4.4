using Godot;
using System;

public partial class Scene2FloorController : BaseFloorController
{
	private double _timer = 0f;
	private bool _isTrigger = false;
	private bool _finishedFloor = false;

	public static Scene2FloorController Instance { get; private set; }

	// Audio players
	private AudioStreamPlayer _finishSound;
	private AudioStreamPlayer _teleportSound;
	

	public override void _Ready()
	{
		if (Instance == null)
		{
			Instance = this;
			GD.Print("FloorController2 Loaded");
		}
		else
		{
			QueueFree();
			return;
		}

		// Get sound nodes
		_finishSound = GetNode<AudioStreamPlayer>("FinishSound");
		_teleportSound = GetNode<AudioStreamPlayer>("TeleportSound");

		SetProcess(true);
	}

	public void OnFinishFloor()
	{
		if (_finishedFloor)
			return;

		_finishedFloor = true;

		if (_finishSound != null)
			_finishSound.Play();

		GD.Print("Floor 2: Finished floor triggered.");
	}

	public override void _Process(double delta)
	{
		if (_finishedFloor && !_isTrigger)
		{
			_timer += delta;

			if (_timer >= 2.0f)
			{
				if (_teleportSound != null)
					_teleportSound.Play();

				LoadFloor(2);
				_isTrigger = true;
				_timer = 2;
			}
		}
	}
}
