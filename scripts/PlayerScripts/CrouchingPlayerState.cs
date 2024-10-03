using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class CrouchingPlayerState : PlayerState
{
    [Export] private float speed = 3.5f;
    [Export] private float acceleration = 0.3f;
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

        player.animationPlayer.Play("crouchingAnimation");
    }   

    public override void Exit()
    {

    }

    public override void Update(float delta)
    {
        base.Update(delta);

        if (Input.IsActionJustReleased("crouch"))
        {
            Uncrouch();
        }
    }

    private async void Uncrouch()
    {
        if(!Input.IsActionPressed("crouch"))
        {
            if (!player.crouchCast.IsColliding())
            {
                player.animationPlayer.PlayBackwards("crouchingAnimation");
                await ToSignal(GetTree().CreateTimer(0.2f), "timeout"); // time of crouch animation
                TransitionToState("IdlePlayerState");
            }
            else
            {
                await ToSignal(GetTree().CreateTimer(0.1f), "timeout");
                Uncrouch();
            }
        }
    }
}
