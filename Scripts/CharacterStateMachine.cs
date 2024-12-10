using Godot;
using System;

public partial class CharacterStateMachine : Node
{
	[Export]
	public NodePath AnimationTreePath;
	[Export]
	public NodePath AnimationPlayerPath;

	private AnimationTree animationTree;
	private AnimationNodeStateMachinePlayback stateMachinePlayback;
	private AnimationPlayer animationPlayer;

	public override void _Ready()
	{
		animationTree = GetNode<AnimationTree>(AnimationTreePath);
		stateMachinePlayback = (AnimationNodeStateMachinePlayback)animationTree.Get("parameters/playback");

		animationPlayer = GetNode<AnimationPlayer>(AnimationPlayerPath);
		// Conectar la señal directamente
		//animationPlayer.Connect("animation_finished", this, nameof(OnAnimationFinished));
	}

	public void ChangeAnimationState(string animationState)
	{
		if (stateMachinePlayback != null)
		{
			stateMachinePlayback.Travel(animationState);
		}
		//Stop pisa a las animaciones de dead y hit
	}

	private void OnAnimationFinished(string animName)
	{
		GD.Print($"Animación {animName} ha terminado.");
		if(animName == "dead"){
			GetParent().QueueFree();
		}
		// Aquí puedes hacer cualquier otra acción basada en la animación que ha terminado.
	}
}
