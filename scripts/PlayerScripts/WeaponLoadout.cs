using Godot;
using System;

public partial class WeaponLoadout : Node3D
{
	[Export] private Flashlight flashlight;

	public void toggle()
	{
		flashlight.toggle();
	}
}
