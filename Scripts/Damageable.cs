using Godot;
using System;

public partial class Damageable : Node
{
	[Export]
	public int health { get; set; } = 40;

	private float _health;

	public override void _Ready() {
		SignalBus.Instance.Connect("OnHealthChanged", this, nameof(OnSignalHealthChanged)); 
	}

	public float Health
	{
	
		get => _health;
		set
		{
			SignalBus.EmitSignal("OnHealthChangedEventHandler", GetParent(), value - _health);
			_health = value;
		}
	}

	public void Hit(int damage)
	{
		_health -= damage;
		if (_health <= 0)
		{
			GetParent().QueueFree();
		}
	}
}
