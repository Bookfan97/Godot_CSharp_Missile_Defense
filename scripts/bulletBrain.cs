using Godot;
using System;

public class bulletBrain : Node
{
    scenes scenes = new scenes();
    bullet _bullet = new bullet();
    private Timer enemySpawner;
   [Export] public float spawnRateDecrease = 0.2f,
        spawnRate = 0,
        maxSpawnRate = 4.0f,
        minSpawnRate = 0.5f;
    [Export] public int playerBulletSpeed = 300;
    [Export] public int enemyBulletSpeed = 300;
    [Export] public int bulletSpeedIncrease = 15,
         maxBulletSpeed = 600;
    //
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        enemySpawner = (Timer)GetNode("enemySpawner");
        spawnRate = maxSpawnRate;
    }

    public void _on_enemySpawner_timeout()
    {
        spawnEnemy();
    }

    public void increaseDifficulty()
    {
        //Increase Bullet Speed
         var newBulletSpeed = enemyBulletSpeed + bulletSpeedIncrease;
         enemyBulletSpeed = Math.Min(newBulletSpeed, maxBulletSpeed);
         
        //Increase Spawn Rate
        var newSpawnRate = spawnRate - spawnRateDecrease;
        newSpawnRate = Math.Max(newSpawnRate, minSpawnRate);
        enemySpawner.WaitTime = newSpawnRate;
        enemySpawner.Start();
    }

    public void spawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(Convert.ToSingle(GD.RandRange(0,1000)), -30);
        Vector2 targetPosition = new Vector2(Convert.ToSingle(GD.RandRange(0,1000)), 550);
        spawnBullet(spawnPosition, targetPosition, "enemy");
    }
    
    public void spawnBullet(Vector2 spawnPosition, Vector2 targetPosition, string animationName)
    {
        //Spawn bullet at position, and look at target position
        var bullet = (bullet)scenes._sceneBullet.Instance();
        GetNode("/root/game/bullets").AddChild(bullet);
        bullet.GlobalPosition = spawnPosition;
        bullet.LookAt(targetPosition);

        //Set the bullet animation
        var bulletSprite = (AnimatedSprite)bullet.GetNode("AnimatedSprite");
        bulletSprite.Play(animationName);

        if (animationName == "player")
        {
            bullet.speed = playerBulletSpeed;
        }
        else if (animationName == "enemy")
        {
            bullet.speed = enemyBulletSpeed;
            GD.Print(enemyBulletSpeed);
        }
    }
    
    public void spawnExplosion(Vector2 spawnPosition, string animationName)
    {
        //Spawn explosion at position
        var explosion = (Area2D)scenes._sceneExplosion.Instance();
        GetNode("/root/game/bullets").AddChild(explosion);
        explosion.GlobalPosition = spawnPosition;

        //Set the explosion animation
        var explosionSprite = (AnimatedSprite)explosion.GetNode("AnimatedSprite");
        explosionSprite.Play(animationName);

    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
