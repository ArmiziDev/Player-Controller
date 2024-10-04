using Godot;
using System;

public partial class PlayerAudios : Node3D
{
    [Export] private AudioStreamPlayer3D playerFootStepAudio { get; set; }
    [Export] private AudioStreamPlayer3D playerJumpingGruntAudio { get; set; }
    public void playFootstep()
    {
        playerFootStepAudio.Play();
    }
    public void jumpGrunt()
    {
        playerJumpingGruntAudio.Play();
    }
}