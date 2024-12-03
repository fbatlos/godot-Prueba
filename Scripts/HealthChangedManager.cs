using Godot;
using System;

public partial class HealthChangedManager : Control
{
	[Export]
	public PackedScene HealthChangedLabelScene = (PackedScene)ResourceLoader.Load("res://Scenes/health_changed_label.tscn");

	private Label healthLabel;

	public override void _Ready() {
		Node labelInstance = HealthChangedLabelScene.Instantiate();
		AddChild(labelInstance);
		
		healthLabel = labelInstance.GetNode<Label>("HitLabel"); 
		
		if (healthLabel == null) { GD.Print("Label 'HitLabel' encontrado correctamente."); } 
	}

	public void OnHealthChanged(int newHealth)
	{
		if (healthLabel != null)
		{
			healthLabel.Text = newHealth.ToString();
		}else{
			GD.Print("Error: HitLabel no encontrado.");
		}
	}
}
