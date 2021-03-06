using Godot;
using System;

public class Item_Dash : RigidBody
{
	private Vector3 pos;
	private float speed;
	private Global global;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		global = (Global) GetNode("/root/Global");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		speed = global.GetPlayerVars().total;
		RotateObjectLocal(Vector3.Up, 3.0f * delta);
		pos = GlobalTransform.origin;
		pos.x += - delta * speed;
		this.Translation = pos;

		if(pos.x < -100) {
			//GetTree().CallGroup("MainListener", "score_increment");
			QueueFree();
		}
	}
	// private void _on_Item_Dash_body_entered(Godot.RigidBody body) {
	//     if(body.IsInGroup("Player")) {
	//         GetTree().CallGroup("MainListener", "dashCount_increment");
	// 		QueueFree();
	// 	}
	// }
}
