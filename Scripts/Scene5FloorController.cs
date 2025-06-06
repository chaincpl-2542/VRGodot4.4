using Godot;

public partial class Scene5FloorController : BaseFloorController
{
	[Export] public Area3D _area;
	private double _timer = 0f;
	private bool _playerInside = false;
	private bool _isTrigger = false;
	private bool _finishedFloor = false;
	public static Scene5FloorController Instance { get; private set; }
	
	public override void _Ready()
	{
		_area = null;
		if (Instance == null)
		{
			Instance = this;
			GD.Print("FloorController5 Loaded");
		}
		else
		{
			QueueFree();
		}
		SetProcess(true);
	}

	public void OnFinishFloor()
	{
		_area.Visible = true;
		_area.Monitoring = true;
		_finishedFloor = true;
		
		_area.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
		_area.Connect("body_exited", new Callable(this, nameof(OnBodyExited)));
	}
	
	public override void LoadFloor(int floorNumber)
	{
		base.LoadFloor(floorNumber);
	}
	
	public override void _Process(double delta)
	{
		if (_finishedFloor)
		{
			if (_playerInside)
			{
				_timer += delta;

				if (_timer >= 2.0f)
				{
					if (!_isTrigger)
					{
						this.LoadFloor(5);
						_isTrigger = true;
					}
					_timer = 2;
				}
			}
		}
	}
	
	private void OnBodyEntered(Node3D body)
	{
		if (body.IsInGroup("Player"))
		{
			_playerInside = true;
			GD.Print("Player entered the floor area!");
		}
	}

	private void OnBodyExited(Node3D body)
	{
		if (body.IsInGroup("Player"))
		{
			_playerInside = false;
			_timer = 0; // Reset timer when player leaves
			GD.Print("Player left the floor area.");
		}
	}
}
