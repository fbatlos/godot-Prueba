using Godot;
using System;

public partial class Damageable : Node
{
	[Export(PropertyHint.Range, "0,100,1")]
	public int MaxHealth { get; set; } = 40;

	[Export]
	public string hit_animation = "hit";

	[Export]
	public string dead_animation = "dead";

	public CharacterStateMachine _characterStateMachine; 

	private int _currentHealth;

	private HealthChangedManager _HealthChangedManager;

	public int CurrentHealth
	{
		get => _currentHealth;
		private set
		{
			_currentHealth = Mathf.Clamp(value, 0, MaxHealth);
			if (_currentHealth <= 0)
			{
				OnDeath();
			}
		}
	}

	public override void _Ready()
	{
		CurrentHealth = MaxHealth;
		_HealthChangedManager = GetNode<HealthChangedManager>("../HealthChangedManager");

		if (_HealthChangedManager == null)
		{
			GD.Print("No se pudo encontrar el nodo HealthChangedManager.");
		}

		_characterStateMachine = GetNode<CharacterStateMachine>("../CharacterStateMachine");

	}

	public void Hit(int damage)
	{
		if (damage < 0)
		{
			GD.PrintErr("No puede ser negativo.");
			return;
		}

		_characterStateMachine.ChangeAnimationState(hit_animation);
		CurrentHealth -= damage;
		_HealthChangedManager?.OnHealthChanged(-damage);
	}

	private void OnDeath()
	{
		GD.Print("Fue eliminado.");
		_characterStateMachine.ChangeAnimationState(dead_animation);
		//GetParent().QueueFree();
	}
}
