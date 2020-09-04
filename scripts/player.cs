using Godot;
using System;

public class player : Node
{
    private bulletBrain _bulletBrain;
    public bool canShoot = true, gameOver = false;
    public int health = 3, score = 0;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _bulletBrain = (bulletBrain)GetNode("/root/game/bullets/bulletBrain");
        updateUI();
    }
    public override void _Input(InputEvent @event)
    {
        //base._Input(@event);
        if (@event.IsActionPressed("click") && gameOver)
        {
            //GD.Print("Left mouse clicked");
            GetTree().ReloadCurrentScene();
        }
    }
    public void _on_playerHitZone_area_entered(Area2D bullet)
    {
        var bulletType = (AnimatedSprite) bullet.GetNodeOrNull("AnimatedSprite");
        if ((bulletType != null) && (bulletType.Animation == "enemy") && (bullet is bullet))
        {
            //_bulletBrain.spawnExplosion(bullet.GlobalPosition, "enemy");
            _bulletBrain.CallDeferred("spawnExplosion", bullet.GlobalPosition, "enemy");
            bullet.QueueFree();
            hitPlayer();
        }
    }

    public void hitPlayer(int damageAmount = 1)
    {
        health = Math.Max(health-damageAmount, 0);
        updateUI();
        if ((health <= 0) && (gameOver != true))
        {
            gameOver = true;
            canShoot = false;
            var gameOverScreen = (Node2D) GetNode("/root/game/HUD/gameOverScreen");
            gameOverScreen.Visible = true;
            var Cannon = (Node2D) GetNode("/root/game/foreground/cannon");
            // _bulletBrain.spawnExplosion(Cannon.GlobalPosition, "enemy");
            _bulletBrain.CallDeferred("spawnExplosion", Cannon.GlobalPosition, "enemy");
            Cannon.QueueFree();
        }
    }

    public void addScore(int scoreAmount = 1)
    {
        score = Math.Max(scoreAmount + score, 0);
        updateUI();
        _bulletBrain.increaseDifficulty();
    }
    
    public void updateUI()
    {
        var healthAndScore = (Label) GetNode("/root/game/HUD/healthAndScore");
        var newHUDtext = "SCORE: " + score + "       " + "HEALTH: " + health;
        healthAndScore.Text = newHUDtext;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
