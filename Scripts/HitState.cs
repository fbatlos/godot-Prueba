using Godot;
using System;

public partial class HitState : Node
{
	public override void _Ready()
	{
		var animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("hit");
	}

   
}
