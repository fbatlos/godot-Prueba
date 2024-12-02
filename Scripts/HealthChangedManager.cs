using Godot;
using System;

public partial class HealthChangedManager : Control
{
	[Export]
	public PackedScene HealthChangedLabelScene;

	private Label healthLabel;

	public override void _Ready()
	{

		var labelInstance = (Control)HealthChangedLabelScene.Instantiate();
		AddChild(labelInstance);

		// Obtener el Label de la escena instanciada
		healthLabel = labelInstance.GetNode<Label>("HitLabel");
	}

	private void OnHealthChanged(int newHealth)
	{
		healthLabel.Text = newHealth.ToString();
	}
}
