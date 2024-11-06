using Godot;
using System;

public partial class CharacterBody2d : CharacterBody2D
{
	[Export]
	public float Speed = 300.0f;
	[Export]
	public float JumpVelocity = -400.0f;
	
	private AnimatedSprite2D animation;
	
	private PackedScene bala;
	
	bool CanTwoJump = true;
	
	public override void _Ready(){
		animation = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		bala = GD.Load<PackedScene>("res://scenes/bala.tscn");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{	
			velocity += GetGravity() * (float)delta;
			animation.Play("jump");
			
			if (Input.IsActionJustPressed("ui_up") && !IsOnFloor())
			{
				if(CanTwoJump == true){
					velocity.Y = JumpVelocity;
					CanTwoJump = false;
				}
			}
			
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_up") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
			animation.FlipH = velocity.X < 0;
			if (IsOnFloor()){
				CanTwoJump = true;
				animation.Play("walk");
			}
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			if (IsOnFloor()){
				CanTwoJump = true;
				animation.Play("idle");
			}
		}
		
		if(Input.IsActionJustPressed("fire")){
			GD.Print("Disparo");
			Node2D instBullet = (Node2D)bala.Instantiate();
			instBullet.GlobalPosition = GlobalPosition;
			

			GetTree().Root.AddChild(instBullet);

		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
