using Godot;
using System;

public partial class BlinkingLight : Node
{
    [Export]
    public Node3D _light;
    
    private Random _random = new Random();
    
    public override void _Ready()
    {
        BlinkLoop();
    }

    private async void BlinkLoop()
    {
        while (true)
        {
            float waitTime = (float)(_random.NextDouble() * 3.0 + 2.0);
            await ToSignal(GetTree().CreateTimer(waitTime), "timeout");

            int flickerCount = _random.Next(2, 7);

            for (int i = 0; i < flickerCount; i++)
            {
                _light.Visible = !_light.Visible;

                float flickerDelay = (float)(_random.NextDouble() * 0.15 + 0.05);
                await ToSignal(GetTree().CreateTimer(flickerDelay), "timeout");
            }

            _light.Visible = _random.Next(0, 2) == 0 ? false : true;
        }
    }
}
