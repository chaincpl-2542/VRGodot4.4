using Godot;
using System.Threading.Tasks;

public partial class Scene3KumanThong : BaseFloorController
{
	[Export] public Area3D area;
	[Export] public Node3D offerNode3D;
	[Export] public MeshInstance3D dialogue;

	[Export] public AudioStreamPlayer3D OfferSound;
	[Export] public AudioStreamPlayer3D LaughSound;
	[Export] public AudioStreamPlayer3D ClueSound;

	[Export] public AudioStreamPlayer3D KumanSound1;
	[Export] public AudioStreamPlayer3D KumanSound2;
	[Export] public AudioStreamPlayer3D KumanSound3;

	[Export] public Area3D areaTrigger;
	[Export] public Node3D kumanParent;

	[Export] public PlayParticle particle;
	
	private TextMesh dialogueText;
	private bool _triggered = false;
	private bool _firstTriggered = false;

	public override void _Ready()
	{
		kumanParent.Visible = false;
		SetProcess(true);
		dialogueText = dialogue.Mesh as TextMesh;
		areaTrigger.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
		area.Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
		dialogueText.Text = "";
	}

	public override void _Input(InputEvent @event)
	{
		if (_triggered) return;

		if (@event is InputEventKey keyEvent && keyEvent.Pressed && keyEvent.Keycode == Key.F6)
		{
			GD.Print("🧪 DEBUG: F6 pressed, simulating offering event.");
			_triggered = true;
			SimulateOffering();
		}
	}

	private void OnBodyEntered(Node3D body)
	{
		if (body.IsInGroup("Offer") && !_triggered)
		{
			_triggered = true;
			offerNode3D.Visible = false;
			body.Visible = false;

			GD.Print("🎁 Offering accepted!");

			SimulateOffering();
		}
		
		if (body.IsInGroup("Player") && !_firstTriggered)
		{
			_firstTriggered = true;
			kumanParent.Visible = true;
			ShowHint();
		}
	}

	private void SimulateOffering()
	{
		OfferSound?.Play();
		LaughSound?.Play();
		ShowHintDelay();
	}

	private void ShowHint()
	{
		particle.Visible = true;
		particle.PlayMyParticle();
		particle.PlayAllParticles();
		KumanSound1?.Play();
		dialogueText.Text =
			"I am not the one doing this. \n" +
			"I can help you, but first \n" +
			"please give me something to drink.";
	}

	private async void ShowHintDelay()
	{
		dialogueText.Text = string.Empty;
		KumanSound2?.Play();
		dialogueText.Text = "Ahh! Thanks! Alright, listen carefully.\n" +
		                    "It looks like you’re trapped in some\n" +
		                    "kind of purgatory mental realm.\n" +
		                    "That Talisman paper at the storage room\n";
		await ToSignal(GetTree().CreateTimer(12), "timeout");

		dialogueText.Text =
			"doesn’t work anymore doesn’t it? It was\n" +
			"there for a reason,\n" +
			"there’s something in there that’s doing this.";
		await ToSignal(GetTree().CreateTimer(7.5), "timeout");

		dialogueText.Text =
			"But to get out of a situation,\n" +
			"you gotta understand what’s\n" +
			"causing it first.";
		await ToSignal(GetTree().CreateTimer(5), "timeout");
		
		KumanSound3?.Play();
		dialogueText.Text =
			"I’m sensing something cryptic on the 3rd floor…\n" +
			"A lot of it too! But I think there’s\n" +
			"only one of them that’s solvable!";
		await ToSignal(GetTree().CreateTimer(9), "timeout");

		dialogueText.Text =
			"You can look in the other rooms for\n" +
			"the passcode clues. I’ll see if I can\n" +
			"figure something out. Good luck\n" +
			"on your part!";
		await ToSignal(GetTree().CreateTimer(8), "timeout");

		Scene3FloorController.Instance.OnFinishFloor();
	}
}
