using Godot;
using System;

public partial class PlayerUi : CanvasLayer
{
	// Called when the node enters the scene tree for the first time.
	Control PlayerInformationNode;
	public override void _Ready()
	{
		PlayerInformationNode = GetNode<Control>("PlayerInformation");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void updatePlayerState(StringName state)
	{
		PlayerInformationNode.GetNode<Label>("PlayerState").Text
			= "Player State: " + state;
	}
}
