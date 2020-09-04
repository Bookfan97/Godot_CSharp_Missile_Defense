using Godot;
using System;

public class player : Node
{
    private bulletBrain _bulletBrain;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _bulletBrain = (bulletBrain)GetNode("/root/game/bullets/bulletBrain");
    }

    public void _on_playerHitZone_area_entered(Area2D bullet)
    {
        var bulletType = (AnimatedSprite) bullet.GetNodeOrNull("AnimatedSprite");
        if ((bulletType != null) && (bulletType.Animation == "enemy") && (bullet is bullet))
        {
            _bulletBrain.spawnExplosion(bullet.GlobalPosition, "enemy");
            bullet.QueueFree();
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
