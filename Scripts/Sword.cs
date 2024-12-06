using Godot;
using System;



public partial class Sword : Area2D
{

	[Export] public int damage = 10;
	

	public override void _Ready()
	{
		Monitoring = false;
	}
	

	public void _on_body_entered(Node body)
	{
		foreach (Node child in body.GetChildren()) { 
			if (child is Damageable) {
				GD.Print(child);
				((Damageable)child).Hit(damage);

				GD.Print(body.Name + "reccive 10 de daño.");
			}
		}
	}
	
}

/* hearbar ,daño , endgame y items
*/
