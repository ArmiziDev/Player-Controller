using Godot;
using System;
using System.Runtime.InteropServices;

public partial class RunningPlayerState : PlayerState
{
    [Export] private float speed = 9.0f;
    [Export] private float acceleration = 0.2f;
    [Export] private float deceleration = 1.0f;
    [Export] private float input_multiplier = 1.0f;
    public override void _Ready()
	{
	}

    public override void Enter(StringName previous_state)
    {
        _speed = speed;
        _acceleration = acceleration;
        _deceleration = deceleration;
        _input_multiplier = input_multiplier;

        player.animationPlayer.Play("sprintingAnimation");
    }

    public override void Exit()
    {

    }

    public override void Update(float delta)
    {
        base.Update(delta);

        if (Input.IsActionJustPressed("jump"))
        {
            TransitionToState("JumpingPlayerState");
        }
        if (Input.IsActionJustReleased("sprint"))
        {
            TransitionToState("WalkingPlayerState");
        }
    }
}
