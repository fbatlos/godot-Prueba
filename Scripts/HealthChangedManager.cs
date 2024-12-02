using Godot;
using System;

public partial class HealthChangedManager : Control
{
	[Export]
	public PackedScene HealthChangedLabel;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		HealthChangedLabel = (PackedScene)GD.Load("res://Scenes/health_changed_label.tscn");
		SignalBus.Instance.Connect("OnHealthChanged", this, nameof(On_signal_health_changed));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void On_signal_health_changed(Node node, int amount_changed)
	{
		Label labelInstance = (Label)HealthChangedLabel.Instance(); 
		node.AddChild(labelInstance); 
		labelInstance.Text = amountChanged.ToString();
	}
}
