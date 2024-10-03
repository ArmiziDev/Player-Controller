using Godot;
using System;

public partial class WalkingPlayerState : PlayerState
{
    [Export] private float speed = 5.0f;
    [Export] private float acceleration = 1.0f;
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

        player.animationPlayer.Play("walkingAnimation");
    }

    public override void Exit()
    {

    }

    public override void Update(float delta)
    {
        base.Update(delta);
        if (player.Velocity.Length() == 0)
        {
            TransitionToState("IdlePlayerState");
        }
        if (Input.IsActionJustPressed("jump"))
        {
            TransitionToState("JumpingPlayerState");
        }
        if (Input.IsActionJustPressed("sprint"))
        {
            TransitionToState("RunningPlayerState");
        }
        if (Input.IsActionJustPressed("crouch"))
        {
            TransitionToState("CrouchingPlayerState");
        }
    }
}
