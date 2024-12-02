using Godot;
using System;

public partial class SignalBus : Node
{
	[Signal] public delegate void OnHealthChangedEventHandler(Node node, int amountChanged);

	public static SignalBus Instance { get; private set; } 
	public override void _Ready() {
		Instance = this; 
	}
	public void EmitSignal(string signal, params object[] args) {
		this.EmitSignal(signal, args);
	}
}
