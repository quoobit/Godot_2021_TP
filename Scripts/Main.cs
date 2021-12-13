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
	float linearSpeedX = 5000f; // dash speed
	float linearSpeedZ = 1500f;
	float linearSpeedY = 2000f;
	private int score;
	private Label scoreLabel;
	private Label dashLabel;
	private Label levelLabel;
	private Label upLabel;
	private int dashCount;
	private int enemyNum;
	private float level;
	private PackedScene EnemyScene = (PackedScene)ResourceLoader.Load("res://Scenes/Enemy.tscn");
	private PackedScene ItemScene = (PackedScene)ResourceLoader.Load("res://Scenes/Item_Dash.tscn");

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		player = (RigidBody)GetNode("Player");
		scoreLabel = (Label)GetNode("Control/ScoreLabel");
		dashLabel = (Label)GetNode("Control/DashLabel");
		levelLabel = (Label)GetNode("Control/LevelLabel");
		upLabel = (Label)GetNode("Control/UpLabel");
		score = 0;
		dashCount = 0;
		enemyNum = 0;
		level = 0;
	}

	public override void _Input(InputEvent @event) {
		if(@event is InputEventKey) {
			var key = (InputEventKey) @event;
			// if(key.IsPressed() && key.IsActionPressed("ui_down")){
			// 	player.SetAxisVelocity(new Vector3(-linearSpeedX * GetProcessDeltaTime(), 0, 0));
			// }
			// if(key.IsPressed() && key.IsActionPressed("ui_up")){
			// 	player.SetAxisVelocity(new Vector3(linearSpeedX * GetProcessDeltaTime(), 0, 0));
			// }
			if(key.IsPressed() && key.IsActionPressed("ui_left")){
				player.SetAxisVelocity(new Vector3(0, 0, - linearSpeedZ * GetProcessDeltaTime()));
			}
			if(key.IsPressed() && key.IsActionPressed("ui_right")){
				player.SetAxisVelocity(new Vector3(0, 0, linearSpeedZ * GetProcessDeltaTime()));
			}
			if(key.IsPressed() && key.IsActionPressed("ui_select")){
				player.SetAxisVelocity(new Vector3(0, linearSpeedY * GetProcessDeltaTime(), 0));
			}
			if(key.IsPressed() && key.IsActionPressed("dash_key")){
				if(dashCount > 0) {
					player.SetAxisVelocity(new Vector3(linearSpeedX * GetProcessDeltaTime(), 0, 0));
					dashCount_increment(-1);
				} else {
					GD.Print("Dash not available!");
				}
			}
		}
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	private void _on_SpawnEnemyTimer_timeout() //시간마다 벽(enemy) spawn
	{
		enemyNum += 1;
		Godot.RigidBody e;
		e = (Godot.RigidBody)EnemyScene.Instance();
		AddChild(e);
		float scale = 1+(float)0.5*level;
		e.Scale = new Vector3(scale, scale, scale);
		if(enemyNum>=10) {
			level += 1;
			GD.Print("level up");
			enemyNum = 0;
			upLabel.Text = "Level Up!";
			levelLabel.Text = "Level : "+level;
		}
		
		if(enemyNum==5) {
			upLabel.Text = "";
		}
		Godot.Vector3 pos = e.GlobalTransform.origin;
		pos.x = 100;
		pos.y = 2;
		int z_posRand = r.Next(-5, 5) * 2;
		int y_posRand = r.Next(0, 5) * 2;
		pos.z = pos.z - z_posRand;
		pos.y = pos.y + y_posRand;

		/* 벽 난이도 설정
		Godot.Vector3 scale = e.Scale;
		int scaleRand = r.Next(1, 3) * 3;
		scale = scale * scaleRand;
		e.Scale = scale;
		*/
		e.Translation = pos;
	}

	private void _on_SpawnItemTimer_timeout() //시간마다 아이템 spawn
	{
		Godot.RigidBody e;
		e = (Godot.RigidBody)ItemScene.Instance();
		AddChild(e);
		Godot.Vector3 pos = e.GlobalTransform.origin;
		pos.x = 100;
		pos.y = 2;

		int posRand = r.Next(-5, 5) * 1;
		pos.z = pos.z - posRand;

		e.Translation = pos;
	}
	
	public void dashCount_increment(int num) {
		if(dashCount < 3) {
			dashCount += num;
		}
		if(dashCount == 3 && num == -1) {
			dashCount += num;
		}
		dashLabel.Text = "Dash : "+dashCount.ToString();
	}

	private void _on_Player_body_entered(Godot.RigidBody body) {
		if(body.IsInGroup("enemyGroup")) {
			GD.Print("Enemy contacted!");
		}
		else if(body.IsInGroup("itemGroup")) {
			GD.Print("Item contacted!");
			dashCount_increment(1);
			body.QueueFree();
		} else {
			//add etc
			//if(hp > 0) GetTree().ReloadCurrentScene();
		}
	}
}





