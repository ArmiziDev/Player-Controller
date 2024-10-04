using Godot;
using System;

public partial class Zombie : CharacterBody3D
{
    public const float Speed = 5.0f;
    public const float JumpVelocity = 4.5f;

    [Export] public Player current_player;
    private NavigationAgent3D navigationAgent; // Navigation agent for pathfinding

    public override void _Ready()
    {
        // Initialize the navigation agent
        navigationAgent = GetNode<NavigationAgent3D>("NavigationAgent3D");
    }

    public override void _PhysicsProcess(double delta)
    {
        // Check if the player exists and update pathfinding target
        if (current_player != null)
        {
            navigationAgent.TargetPosition = current_player.GlobalTransform.Origin;
            chasePlayer((float)delta);
        }
        ApplyGravity((float)delta);
        MoveAndSlide();
    }

    private void ApplyGravity(float delta)
    {
        Vector3 velocity = Velocity;
        // Add gravity if the zombie is not on the floor.
        if (!IsOnFloor())
        {
            Velocity += GetGravity() * delta;
        }
    }

    public void chasePlayer(float delta)
    {
        // Check if the navigation agent is close enough to the player (target position).
        if (!navigationAgent.IsTargetReached())
        {
            // Get the next location on the path towards the target
            Vector3 nextPosition = navigationAgent.GetNextPathPosition();

            // Calculate direction to the next point on the path
            Vector3 direction = (nextPosition - GlobalTransform.Origin).Normalized();

            // Move towards the next position
            Velocity = direction * Speed;
        }
    }

    // Function to set the current player target
    public void SetTargetPlayer(Player player)
    {
        current_player = player;
    }
}
