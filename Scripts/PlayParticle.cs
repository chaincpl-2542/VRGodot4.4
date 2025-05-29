using Godot;
using System;

public partial class PlayParticle : Node3D
{
    [Export] public AnimationPlayer Anim;
    [Export] public GpuParticles3D Particles1;
    [Export] public GpuParticles3D Particles2;
    public void PlayMyParticle()
    {
        Particles1.Restart();
        Particles2.Restart();
        if (Anim != null)
        {
            Anim?.Play("Pick Up");
        }
    }

    public void PlayAllParticles()
    {
        foreach (var child in GetChildren())
        {
            if (child is GpuParticles3D particle)
            {
                particle.Restart();
            }
        }
    }
}
