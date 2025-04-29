using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMainHeroData", menuName = "Data/Player Data/Base Data")]
public class MainHeroData : ScriptableObject
{
    [Header("Move State")]
    public float MovementVelocity = 10.0f;

    [Header("Jump State")]
    public float JumpVelocity = 15.0f;
    public int AmountOfJumps = 1;

    [Header("Wall Jump State")]
    public float WallJumpVelocity = 20.0f;
    public float WallJumpTime = 0.4f;
    public Vector2 WallJumpAngle = new Vector2(1, 2);

    [Header("In Air State")]
    public float CoyoteTime = 0.2f;
    public float JumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float WallSlideVelocity = 3.0f;

    [Header("Wall Climb State")]
    public float WallClimbVelocity = 3.0f;

    [Header("Ledge Climb State")]
    public Vector2 StartOffset;
    public Vector2 StopOffset;

    [Header("Dash State")]
    public float DashCooldown = 2.0f;
    public float MaxHoldTime = 1.0f;
    public float HoldTimeScale = 0.25f;
    public float DashTime = 0.2f;
    public float DashVelocity = 30.0f;
    public float Drag = 10.0f;
    public float DashEndVerticalMultiplier = 0.2f;
    public float DistanceBetweenAfterImages = 0.5f;

    [Header("Crouch States")]
    public float CrouchMovementVelocity = 5.0f;
    public float CrouchColliderHeight = 0.8f;
    public float StandColliderHeight = 1.6f;
}
