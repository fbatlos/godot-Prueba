using Godot;
using System;

public partial class VisionEnemy : Area2D
{
	[Export]
	public AudioStreamPlayer2D sound;

	public Node2D player;

	public bool continue_walk;

	public bool stop = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//GD.Print(player.GlobalPosition);
	}
	
	
	public void playerIsInside(Node2D body){
		if(body.Name == "Player"){
			player = body;
			if(GetParent().Name == "Boar"){
				sound.Play();
			}
			//GD.Print(player.GlobalPosition);
			continue_walk = true;
			stop = false;

		}
	}

	public void playerIsOut(Node2D body){
		if (body == player) {
			// GD.Print("Player fuera"); 
			player = null; 
			continue_walk =false;
			stop = true;
		}
	}

}
