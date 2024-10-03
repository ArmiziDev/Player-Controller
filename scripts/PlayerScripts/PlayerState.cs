using Godot;
using System;
using System.Runtime.InteropServices;

public partial class PlayerState : Node
{
    public Player player;

    public float _speed, _acceleration, _deceleration, _input_multiplier;

    [Signal] public delegate void TransitionStateSignalEventHandler(StringName state);

    virtual public void Enter(StringName previous_state)
    {

    }

    virtual public void Exit() 
    { 
    }

    virtual public void Update(float delta)
    {
        PlayerMovement(delta);
        PlayerInputs(delta);
    }

    public void TransitionToState(StringName state)
    {
        EmitSignal(SignalName.TransitionStateSignal, state);
    }

    private void PlayerMovement(float d)
    {
        player.UpdateInput(_speed, _acceleration, _deceleration, _input_multiplier);
        player.UpdateGravity(d);
        player.UpdateVelocity();
    }
    private void PlayerInputs(float delta)
    {
        if (Input.IsActionJustPressed("use"))
        {
            player.weaponLoadout.toggle();
        }
    }
}
