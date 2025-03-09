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

	private bool stop;
	
	[Export]
	public NodePath VisionPath;

	[Export]
	public AudioStreamPlayer2D sound;

	[Export]
	public Sprite2D sprite;
	
	[Export]
	public Area2D attack;

	public VisionEnemy visionNodo;

	public Vector2 starting_move;

	public Node2D player;

	public CharacterStateMachine _characterStateMachine;

	private Vector2 direction;

	public Damageable _isDead;
	

	public override void _Ready()
	{
		_characterStateMachine = GetNode<CharacterStateMachine>("CharacterStateMachine");
		_isDead = GetNode<Damageable>("Damageable");
	}

	public override void _Process(double delta)
	{
		if(_isDead.dead == false){

			visionNodo = GetNode<VisionEnemy>(VisionPath); 
			player = visionNodo.player;
			continue_walk = visionNodo.continue_walk;
			stop = visionNodo.stop;
			if(continue_walk){
				//timerWait = 0.8;
				OnContinue(delta);
			}
			if(player != null){
				starting_move =	player.GlobalPosition;
			}
		}
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
		if(player == null  || (continue_walk || _isDead.dead) ){
			Vector2 enemyPosition = GlobalPosition;
			direction = new Vector2(0,0).Normalized();
		}else{
			Vector2 enemyPosition = GlobalPosition;
			direction = new Vector2(starting_move.X - enemyPosition.X,0).Normalized();
		}

		if(_isDead.hit){
			
			_characterStateMachine.ChangeAnimationState("hit");
			
			if(sprite.FlipH){
				velocity +=new Vector2(50,-60) ;
				Position += new Vector2(-30,0);
				//GD.Print(velocity);
			}
			else
			{
				velocity +=new Vector2(50,-60);
				Position += new Vector2(30,0);
				//GD.Print(velocity);
			}
			_isDead.hit = false;
		}
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		else if (direction != Vector2.Zero && _isDead.hit == false)
		{
			//if(_isDead.dead){GD.Print(direction);}

			velocity.X = direction.X * Speed;

			if (direction.X < 0){
				sprite.FlipH= false;
				attack.Position = new Vector2(0f, attack.Position.Y);
			}
		 	else if (direction.X > 0){
				sprite.FlipH = true;
				attack.Position = new Vector2(9f, attack.Position.Y); 
			}
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			if(stop){
				_characterStateMachine.ChangeAnimationState(name_Animation_Stop);
			}
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
