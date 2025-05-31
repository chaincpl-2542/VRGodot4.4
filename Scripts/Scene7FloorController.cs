using System.Threading.Tasks;
using Godot;

public partial class Scene7FloorController : BaseFloorController
{
	[Export] public Area3D _area;
	private bool _isTrigger = false;
	public static Scene7FloorController Instance { get; private set; }
	
	public override void _Ready()
	{
		if (Instance == null)
		{
			Instance = this;
			GD.Print("FloorController7 Loaded");
		}
		else
		{
			QueueFree();
		}
		SetProcess(true);
	}
	
	public void OnFinishFloor()
	{
		if (_area.IsConnected("body_entered", new Callable(this, nameof(OnBodyEntered))))
			_area.Disconnect("body_entered", new Callable(this, nameof(OnBodyEntered)));

		_area.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
	}
	
	public async Task OnBodyEntered(Node3D body)
	{
		if (body.IsInGroup("Player"))
		{
			this.ForceLoadFloor(7);
		}
	}
}
