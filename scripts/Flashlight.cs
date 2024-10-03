using Godot;
using System;

public partial class Flashlight : Node3D
{
	// Called when the node enters the scene tree for the first time.
	private SpotLight3D _light;

	[Export] AudioStreamPlayer3D audioGameOutput;
	[Export] AudioStream flashlightClickNoise;

	public override void _Ready()
	{
		_light = GetNode<SpotLight3D>("%SpotLight3D");
		audioGameOutput.Stream = flashlightClickNoise;
	}

	public void toggle()
	{
		_light.Visible = !_light.Visible;
        audioGameOutput.Play();
    }

	public bool getFashlight()
	{
		return _light.Visible;
	}
}
