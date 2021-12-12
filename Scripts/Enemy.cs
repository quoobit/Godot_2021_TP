using Godot;
using System;

public class Enemy : RigidBody
{
	private Vector3 pos;
	private float speed = 50;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		pos = GlobalTransform.origin;
		pos.x += - delta * speed;
		this.Translation = pos;
		if(pos.x < -100) {
			GetTree().CallGroup("ScoreListener", "score_increment");
			GD.Print("call");
			QueueFree();
		}
	}
}
