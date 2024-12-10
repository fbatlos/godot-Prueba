using Godot;
using System;

public partial class DeadState : Node
{
	public override void _Ready()
	{
		var animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("dead");
	}
	
}
