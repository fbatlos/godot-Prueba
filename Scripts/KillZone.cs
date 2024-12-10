using Godot;
using System;

public partial class KillZone : Area2D
{
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void _OnBodyEnter(Node2D body){
		GD.Print(body.Name);
		if (body.Name == "Player"){
			GetTree().ChangeSceneToFile("res://Scenes/game_over.tscn");
		}
		else
		{
			body.QueueFree();
		}
	}
}