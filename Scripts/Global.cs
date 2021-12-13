using System;
using Godot;
//Enemy 속도 Class
public class Global : Node
{

	private PlayerVars playerVars;

	public override void _Ready()
	{

	playerVars = new PlayerVars();

	playerVars.total = 20;

	}

	public PlayerVars GetPlayerVars(){
	return playerVars;
	}
	
	public void AddPlayerVars(int i){
		playerVars.total += i;
	}


}

public class PlayerVars {
  public int total;
}
