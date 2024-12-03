using Godot;
using System;

public partial class Damageable : Node
{
	[Export(PropertyHint.Range, "0,100,1")] // Agregamos un rango para la salud
	public int MaxHealth { get; set; } = 40;

	private int _currentHealth;

	private HealthChangedManager _HealthChangedManager;

	public int CurrentHealth
	{
		get => _currentHealth;
		private set
		{
			_currentHealth = Mathf.Clamp(value, 0, MaxHealth); // Clampea el valor para evitar números inválidos
			if (_currentHealth <= 0)
			{
				OnDeath();
			}
		}
	}

	public override void _Ready()
	{
		// Inicializamos la salud actual
		CurrentHealth = MaxHealth;
		_HealthChangedManager = GetNode<HealthChangedManager>("../HealthChangedManager");
		if(_HealthChangedManager == null){
			GD.Print("No se pudo encontrar el nodo HealthChangedManager.");
		}
	}

	public void Hit(int damage)
	{
		if (damage < 0)
		{
			GD.PrintErr("No puede ser negativo.");
			return;
		}

		CurrentHealth -= damage;
		_HealthChangedManager.OnHealthChanged(-damage);
	}

	private void OnDeath()
	{
		GD.Print("Fue eliminado.");
		GetParent().QueueFree(); // Eliminamos el nodo
	}
}
