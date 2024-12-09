using Godot;
using System;

public partial class health_bar : TextureProgressBar
{
	[Export]
	public Damageable damageable;

	public override void _Ready() {
		MaxValue = damageable.MaxHealth;		
	}
	
	public void updateHealth(int health){
		Value = health;
	}
}
