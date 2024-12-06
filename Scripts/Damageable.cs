using Godot;
using System;
using System.Threading.Tasks;

public partial class Damageable : Node
{
	[Export(PropertyHint.Range, "0,100,1")]
	public int MaxHealth { get; set; } = 40;

	[Export]
	public health_bar health_bar;

	public const string hit_animation = "hit";
	public const string dead_animation = "dead";

	[Export]
	public CharacterStateMachine characterStateMachine;


	private int _currentHealth;

	[Export]
	public HealthChangedManager _HealthChangedManager;

	public bool dead = false;

	public bool hit = false ;

	private double timerDeath = 0.8;

	public int CurrentHealth
	{
		get => _currentHealth;
		private set
		{
			_currentHealth = Mathf.Clamp(value, 0, MaxHealth);
			if (_currentHealth <= 0)
			{
				dead =true;
			}
		}
	}

	public override void _Ready()
	{
		CurrentHealth = MaxHealth;
		//_HealthChangedManager = GetNode<HealthChangedManager>("../HealthChangedManager");

		if (_HealthChangedManager == null)
		{
			GD.Print("No se pudo encontrar el nodo HealthChangedManager.");
		}

		//_characterStateMachine = GetNode<CharacterStateMachine>("../CharacterStateMachine");
	}

	public override void _Process(double delta)
	{
		if(dead){
			OnDeath(delta);
		}
		health_bar.updateHealth(_currentHealth);
	}

	public void Hit(int damage)
	{
		hit = true;
		//characterStateMachine.ChangeAnimationState(hit_animation);
		CurrentHealth -= damage;
		_HealthChangedManager?.OnHealthChanged(-damage);
		
		
	}

	private void OnDeath(double delta)
	{
		characterStateMachine.ChangeAnimationState(dead_animation);
		timerDeath -= delta;
		if(timerDeath <= 0){
			if(GetParent().Name != "Player"){
				GetParent().QueueFree();
				dead = false;
			}else{
				GetTree().ChangeSceneToFile("res://Scenes/game_over.tscn");
			}
			
		}
	}
}
