using Godot;
using System;
using System.Threading.Tasks;

public partial class Damageable : Node
{
	[Export(PropertyHint.Range, "0,100,1")]
	public int MaxHealth { get; set; } = 40;

	[Export]
	public health_bar health_bar;

	[Export]
	public HealthChangedManager _HealthChangedManager;

	[Export]
	public CharacterStateMachine characterStateMachine;

	[Export]
	public AnimationPlayer animationPlayer;
	
	public const string hit_animation = "hit";
	public const string dead_animation = "dead";

	private int _currentHealth;

	

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

		if (_HealthChangedManager == null)
		{
			GD.Print("No se pudo encontrar el nodo HealthChangedManager.");
		}

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
		if(GetParent().Name != "Player"){
			if(GetParent().Name == "Boar"){
				characterStateMachine.ChangeAnimationState("hit");
			}	
		}
		else
		{
			animationPlayer.Play(hit_animation);
		}
		CurrentHealth -= damage;
		_HealthChangedManager?.OnHealthChanged(-damage);
	}

	private void OnDeath(double delta)
	{

		if(GetParent().Name != "Player"){
		
			if(GetParent().Name == "Boar"){
				
				characterStateMachine.ChangeAnimationState("hit");
			}
			else
			{
				characterStateMachine.ChangeAnimationState(dead_animation);
			}
		}
		else
		{
			animationPlayer.Play(dead_animation);
		}
		timerDeath -= delta;
		if(timerDeath <= 0){
			if(GetParent().Name != "Player"){
				GetParent().QueueFree();
				if(GetParent().Name == "Boar"){
					GetTree().ChangeSceneToFile("res://Scenes/game_win.tscn");
				}
				dead = false;
			}else{
				GetTree().ChangeSceneToFile("res://Scenes/game_over.tscn");
			}
			
		}
	}
}
