using Godot;
using System;

public partial class VisionEnemy : Area2D
{
	public Vector2 playerEntred ;
	
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	
	
	public void playerIsInside(Node2D body){
		if(body.Name == "Player"){
			GD.Print(body.GlobalPosition);
			playerEntred = body.GlobalPosition;
		}

	}

}
