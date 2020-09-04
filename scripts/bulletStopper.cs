using Godot;
using System;

public class bulletStopper : Area2D
{
    private bulletBrain _bulletBrain;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _bulletBrain = (bulletBrain)GetNode("/root/game/bullets/bulletBrain");
    }

    public void _on_bulletStopper_area_entered(Area2D bullet)
    {
        var bulletType = (AnimatedSprite) bullet.GetNodeOrNull("AnimatedSprite");
        if ((bulletType != null) && (bulletType.Animation == "player") && (bullet is bullet))
        {
            _bulletBrain.spawnExplosion(GlobalPosition, "player");
            bullet.QueueFree();
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
