using Godot;
using System;

public partial class WalkState : Node
{
	public override void _Ready()
	{
		var animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		animationPlayer.Play("walk");
	}

}
