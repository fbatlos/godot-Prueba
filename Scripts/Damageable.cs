using Godot;
using System;

public partial class Damageable : Node
{
    [Export(PropertyHint.Range, "0,100,1")] // Agregamos un rango para la salud
    public int MaxHealth { get; set; } = 40;

    private int _currentHealth;

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
    }

    public void Hit(int damage)
    {
        if (damage < 0)
        {
            GD.PrintErr("Damage cannot be negative.");
            return;
        }

        CurrentHealth -= damage;
        GD.Print($"Health: {CurrentHealth}");
    }

    private void OnDeath()
    {
        GD.Print("Damageable has been destroyed.");
        GetParent().QueueFree(); // Eliminamos el nodo
    }
}

