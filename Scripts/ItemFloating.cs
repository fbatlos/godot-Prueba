using Godot;
using System;

public partial class ItemFloating : AnimatedSprite2D
{
	private float _startY;
	private float _minY = -3;
	private float _maxY = 0;
	private float _speed = 3.5f;
	private bool _goingUp = true;

	public override void _Ready()
	{
		_startY = Position.Y;
	}

	public override void _Process(double delta)
	{
		float newPosition = (float)(_speed * delta);
		if (_goingUp)
		{
			Position = new Vector2(Position.X, Position.Y - newPosition);
			if (Position.Y <= _startY + _minY)
			{
				_goingUp = false;
			}
		}
		else
		{
			
			Position = new Vector2(Position.X, Position.Y + newPosition);
			if (Position.Y >= _startY + _maxY)
			{
				_goingUp = true;
			}
		}
	}
}
