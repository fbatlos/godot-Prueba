using Godot;
using System;

public partial class Snail : CharacterBody2D
{
	public const float Speed = 50.0f;
	
	
	public Vector2 starting_move = new VisionEnemy().playerEntred;


	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 enemyPosition = GlobalPosition;
		GD.Print(starting_move);
		Vector2 direction = new Vector2(starting_move.X,0).Normalized();
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
