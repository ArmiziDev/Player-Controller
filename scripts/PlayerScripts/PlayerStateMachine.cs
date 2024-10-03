using Godot;
using System;
using System.Collections.Generic;
using static Godot.WebSocketPeer;

public partial class PlayerStateMachine : Node
{
	private Dictionary<StringName, PlayerState> states = new Dictionary<StringName, PlayerState>();

    private PlayerState currentState;

    [Export] Player player;


    public override void _Ready()
    {
        foreach (PlayerState state in GetChildren())
        {
            state.player = this.player;
            state.TransitionStateSignal += TransitionToState;

            states.Add(state.Name,state);  
        }
        if (states.Count > 0)
        {
            // Initialize Player To Idle
            currentState = states["IdlePlayerState"];
            currentState.Enter(currentState.Name);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        currentState.Update((float)delta);
    }

    public void TransitionToState(StringName state)
	{
        currentState.Exit();

        PlayerState previous_state = currentState;
        currentState = states[state];

        currentState.Enter(previous_state.Name);

        //GD.Print("Transition to Player State: " +  state);
        player.playerUI.updatePlayerState(state);
	}
}
