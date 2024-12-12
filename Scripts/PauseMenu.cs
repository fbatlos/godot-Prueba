using Godot;
using System;

public partial class PauseMenu : Control
{
	[Export]
	AnimationPlayer animation;
	
	public void Resume()
	{
		GetTree().Paused = false;
		animation.Play("RESET");
		//animation.PlayBackwards("blur");
	}

	public void Pause()
	{
		GetTree().Paused = true;
		animation.Play("blur");
	}
	
	public void TestEsc()
	{
		if (Input.IsActionJustPressed("esc") && !GetTree().Paused)
		{
			Pause();
		}
		else if (Input.IsActionJustPressed("esc") && GetTree().Paused)
		{
			Resume();
		}
	}
	
	
	public void _on_Resume(){
		Resume();
	}
	
	public void _on_Restart(){
		Resume();
		GetTree().ReloadCurrentScene();
	}
	
	public void _on_Quit(){
		GetTree().Quit();
	}
	
	public override void _Process(double delta)
	{
		TestEsc();
	}
	
	public override void _Ready()
	{
		animation.Play("RESET");
	}


}
