using Godot;
using System;

public partial class Snail : CharacterBody2D
{
	public const float Speed = 50.0f;
	public const string name_Animation_Stop = "stop";
	public  const string name_Animation_Walk = "walk";

	public  const string name_Animation_Continue_Walk = "continue_walk";
	private double timerWait = 0.8;

	private bool continue_walk;
	
	[Export]
	public NodePath VisionPath;

	[Export]
	public Sprite2D sprite;

	public VisionEnemy visionNodo;

	public Vector2 starting_move;

	public Node2D player;

	public CharacterStateMachine _characterStateMachine;

	private Vector2 direction;

	public override void _Ready()
	{
		_characterStateMachine = GetNode<CharacterStateMachine>("CharacterStateMachine");	
	}

	public override void _Process(double delta)
	{
		visionNodo = GetNode<VisionEnemy>(VisionPath); 
		player = visionNodo.player;
		continue_walk = visionNodo.continue_walk;
		if(continue_walk){
			OnContinue(delta);
		}
		starting_move = player.GlobalPosition;
	}


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		//No se mueve 
		if(player == null  || continue_walk == true){
			Vector2 enemyPosition = GlobalPosition;
			direction = new Vector2(0,0).Normalized();
			
			
		}else{
			Vector2 enemyPosition = GlobalPosition;
			direction = new Vector2(starting_move.X - enemyPosition.X,0).Normalized();
		}
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			if (direction.X < 0){
				sprite.FlipH= false;
			}
		 	else if (direction.X > 0){
				sprite.FlipH = true;
			}
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			_characterStateMachine.ChangeAnimationState(name_Animation_Stop);
		}

		Velocity = velocity;
		MoveAndSlide();
	}


	private void OnContinue(double delta)
	{
		_characterStateMachine.ChangeAnimationState(name_Animation_Continue_Walk);
		timerWait -= delta;
		if(timerWait <= 0){
			continue_walk = false;
			_characterStateMachine.ChangeAnimationState(name_Animation_Walk);
		}
	}
}
