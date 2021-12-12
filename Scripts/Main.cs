using Godot;
using System;
using System.Diagnostics;
public class Main : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	//플레이어
	RigidBody player;
	Random r = new Random();
	float linearSpeedX  = 1500f;
	float linearSpeedY = 2000f;

	private PackedScene EnemyScene = (PackedScene)ResourceLoader.Load("res://Scenes/Enemy.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = (RigidBody)GetNode("Player");

	}

	public override void _Input(InputEvent @event) {
		if(@event is InputEventKey) {
			var key = (InputEventKey) @event;
			if(key.IsPressed() && key.IsActionPressed("ui_down")){
				//GD.Print("left");
				player.SetAxisVelocity(new Vector3(-linearSpeedX * GetProcessDeltaTime(), 0, 0));
			}
			if(key.IsPressed() && key.IsActionPressed("ui_up")){
				player.SetAxisVelocity(new Vector3(linearSpeedX * GetProcessDeltaTime(), 0, 0));
			}
			if(key.IsPressed() && key.IsActionPressed("ui_left")){
				player.SetAxisVelocity(new Vector3(0, 0, - linearSpeedX * GetProcessDeltaTime()));
			}
			if(key.IsPressed() && key.IsActionPressed("ui_right")){
				player.SetAxisVelocity(new Vector3(0, 0, linearSpeedX * GetProcessDeltaTime()));
			}
			if(key.IsPressed() && key.IsActionPressed("ui_select")){
				player.SetAxisVelocity(new Vector3(0, linearSpeedY * GetProcessDeltaTime(), 0));
			}
		}
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	private void _on_SpawnEnemyTimer_timeout() //시간마다 spawn
	{
		Godot.RigidBody e;
		e = (Godot.RigidBody)EnemyScene.Instance();
		AddChild(e);
		Godot.Vector3 pos = e.GlobalTransform.origin;
		pos.x = 100;
		pos.y = 2;

		int posRand = r.Next(-5, 5) * 2;
		pos.z = pos.z - posRand;

		Godot.Vector3 scale = e.Scale;
		int scaleRand = r.Next(1, 3) * 3;
		scale = scale * scaleRand;
		e.Scale = scale;

		e.Translation = pos;
	}



	private void _on_Player_body_entered(Godot.RigidBody body) {
		if(body.IsInGroup("enemyGroup")) {
			GD.Print("Enemy contacted!");
		}
		else {
			//add heart and etc
			//if(hp > 0) GetTree().ReloadCurrentScene();
		}
	}
}



