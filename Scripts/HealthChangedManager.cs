using Godot;
using System;

public partial class HealthChangedManager : Control
{
	[Export]
	public PackedScene HealthChangedLabelScene = (PackedScene)ResourceLoader.Load("res://Scenes/health_changed_label.tscn");

	[Export] public Color damage_color = new Color(1, 0, 0); // DARK_RED 
	[Export] public Color heal_color = new Color(0, 0.5f, 0); // DARK_GREEN


	private Label healthLabel;



	public override void _Ready() {
	}

	public void OnHealthChanged(int newHealth)
	{
		Node labelInstance = HealthChangedLabelScene.Instantiate();
		AddChild(labelInstance);
		
		healthLabel = labelInstance.GetNode<Label>("HitLabel");
		healthLabel.Position = new Vector2(0,0);
		
		if (healthLabel != null)
		{
			healthLabel.Text = newHealth.ToString();
			
			if (newHealth >= 0){
				healthLabel.Modulate = heal_color;
			}
			else
			{
				healthLabel.Modulate = damage_color;
			}

		}else{
			GD.Print("Error: HitLabel no encontrado.");
		}
	}
}
