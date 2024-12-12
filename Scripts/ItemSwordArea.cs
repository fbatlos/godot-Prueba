using Godot;
using System;

public partial class ItemSwordArea : Area2D
{
	[Export]
	AnimationPlayer animation;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void _on_Player_Get(Node body){
		if(body.Name == "Player"){
			foreach (Node child in body.GetChildren()){
				if (child is Sword) {
					((Sword)child).damage += 10;
					((Sword)child).itemSwordVisible.Visible = true;
					GetParent().QueueFree();
				}
			}
		}
	}
	
}
