using Godot;
using System;

public partial class Player : CharacterBody3D
{
    [Export] public Camera3D Camera { get; set; }
    [Export] public float MouseSensitivity = 0.1f; // Sensitivity for mouse movement
    [Export] public AnimationPlayer animationPlayer { get; set; }
    [Export] public WeaponLoadout weaponLoadout { get; set; }
    [Export] public PlayerUi playerUI { get; set; }

    private Vector3 rotationDegreesLocal; // Store the local rotation to prevent gimbal lock

    public ShapeCast3D crouchCast { get; set; }
    
    public override void _Ready()
    {
        // Lock the mouse cursor to the center of the screen
        Input.MouseMode = Input.MouseModeEnum.Captured;
        crouchCast = GetNode<ShapeCast3D>("%CrouchCast");
    }

    public override void _PhysicsProcess(double delta)
    {

    }

    public void UpdateInput(float speed, float acceleration, float deceleration, float input_multiplier = 1.0f)
    {
        Vector3 velocity = Velocity;

        Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_backward");
        Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized() * input_multiplier;

        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * speed;
            velocity.Z = direction.Z * speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
            velocity.Z = Mathf.MoveToward(Velocity.Z, 0, speed);
        }

        Velocity = velocity;
    }
    public void UpdateGravity(float delta)
    {
        // Add the gravity.
        if (!IsOnFloor())
        {
            Velocity += GetGravity() * (float)delta;
        }
    }
    public void UpdateVelocity()
    {
        MoveAndSlide();
    }

    public override void _Input(InputEvent @event)
    {
        // Check if the event is a mouse motion event
        if (@event is InputEventMouseMotion mouseEvent)
        {
            // Get the mouse movement
            Vector2 mouseMovement = mouseEvent.Relative;

            // Rotate the player horizontally (yaw) based on the mouse's x movement
            rotationDegreesLocal.Y -= mouseMovement.X * MouseSensitivity;

            // Rotate the camera vertically (pitch) based on the mouse's y movement
            float pitch = Camera.RotationDegrees.X - mouseMovement.Y * MouseSensitivity;
            pitch = Mathf.Clamp(pitch, -90.0f, 90.0f); // Clamp the vertical rotation to prevent flipping

            // Apply the rotation to the camera
            Camera.RotationDegrees = new Vector3(pitch, Camera.RotationDegrees.Y, Camera.RotationDegrees.Z);

            // Apply the rotation to the player body (y-axis only)
            RotationDegrees = new Vector3(0, rotationDegreesLocal.Y, 0);
        }
    }
}
