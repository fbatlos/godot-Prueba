using Godot;
using System;

public partial class HitLabel : Label
{
	[Export]
	float speed = 10.5f;
	
	Vector2 posicionSpawn = new Vector2(0,1);
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position += posicionSpawn * (float)delta * speed;
		//GD.Print(Position);
		
		if(Position > new Vector2(0, 15)){
			QueueFree();
		}
	}
}
