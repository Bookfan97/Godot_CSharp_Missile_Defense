using Godot;
using System;

public class bulletBrain : Node
{
    scenes scenes = new scenes();
    bullet _bullet = new bullet();
    private Timer enemySpawner;
    public float spawnRateDecrease = 0.2f,
        spawnRate = 0,
        maxSpawnRate = 4.0f,
        minSpawnRate = 0.5f;
    // public int bulletSpeedIncrease = 20,
    //     maxBulletSpeed = 600, 
    //     minBulletSpeed = 300;
    //
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        enemySpawner = (Timer)GetNode("enemySpawner");
        spawnRate = maxSpawnRate;
        // minBulletSpeed = _bullet.speed;
    }

    public void _on_enemySpawner_timeout()
    {
        spawnEnemy();
    }

    public void increaseDifficulty()
    {
        //Increase Bullet Speed
        // var bullet = (bullet)scenes._sceneBullet.Instance();
        // var newBulletSpeed = bullet.GetSpeed() + bulletSpeedIncrease;
        // bullet.SetSpeed(Math.Min(newBulletSpeed, maxBulletSpeed));
        // GD.Print("new: "+ newBulletSpeed+ " Speed: "+ bullet.GetSpeed());
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
