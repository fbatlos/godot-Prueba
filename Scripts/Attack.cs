using Godot;
using System;

public partial class Attack : Area2D
{
	[Export]
	public int damage = 10;

	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Monitoring = false;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void playerIsInside(Node body){
		if(body.Name == "Player"){
			foreach (Node child in body.GetChildren()) {
			if (child is Damageable) {
				((Damageable)child).Hit(damage);
				GD.Print(body.Name + "reccive 10 de daño.");
			}
		}
		}
	}

}
