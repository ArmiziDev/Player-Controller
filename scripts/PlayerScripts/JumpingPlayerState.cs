using Godot;
using System;

public partial class JumpingPlayerState : PlayerState
{
    [Export] private float speed = 5.0f;
    [Export] private float acceleration = 1.0f;
    [Export] private float deceleration = 1.0f;
    [Export] private float input_multiplier = 1.0f;
    [Export] private float jump_velocity = 4.0f;
    public override void _Ready()
	{
	}

    public override void Enter(StringName previous_state)
    {
        _speed = speed;
        _acceleration = acceleration;
        _deceleration = deceleration;
        _input_multiplier = input_multiplier;

        player.Velocity += new Vector3(0.0f, jump_velocity, 0.0f);

        player.animationPlayer.Play("jumpingAnimation");
    }

    public override void Exit()
    {

    }

    public override void Update(float delta)
    {
        base.Update(delta);
        if (player.IsOnFloor())
        {
            TransitionToState("IdlePlayerState");
        }
    }

}
