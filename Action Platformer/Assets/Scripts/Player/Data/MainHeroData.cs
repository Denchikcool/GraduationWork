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

    [Header("In Air State")]
    public float CoyoteTime = 0.2f;
    public float JumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float WallSlideVelocity = 3.0f;

    [Header("Wall Climb State")]
    public float WallClimbVelocity = 3.0f;

    [Header("Check Variables")]
    public float GroundCheckRadius = 0.3f;
    public LayerMask WhatIsGround;
    public float WallCheckDistance = 0.5f;
}
