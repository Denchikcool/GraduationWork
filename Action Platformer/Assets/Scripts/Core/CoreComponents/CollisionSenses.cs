using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region Transforms
    public Transform GroundCheck { get => _groundCheck; private set => _groundCheck = value; }
    public Transform WallCheck { get => _wallCheck; private set => _wallCheck = value; }
    public Transform LedgeCheck { get => _ledgeCheck; private set => _ledgeCheck = value; }
    public Transform UpHeadCheck { get => _upHeadCheck; private set => _upHeadCheck = value; }

    public float GroundCheckRadius { get => _groundCheckRadius; set => _groundCheckRadius = value; }
    public float WallCheckDistance { get => _wallCheckDistance; set => _wallCheckDistance = value; }
    public LayerMask WhatIsGround { get => _whatIsGround; set => _whatIsGround = value; }

    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private Transform _wallCheck;
    [SerializeField]
    private Transform _ledgeCheck;
    [SerializeField]
    private Transform _upHeadCheck;

    #endregion

    #region Check Variables
    [SerializeField]
    private float _groundCheckRadius;
    [SerializeField]
    private float _wallCheckDistance;

    [SerializeField]
    private LayerMask _whatIsGround;
    #endregion

    #region Check Properties
    public bool HeadTouchingWall
    {
        get => Physics2D.OverlapCircle(_upHeadCheck.position, _groundCheckRadius, _whatIsGround);
    }

    public bool TouchingGround
    {
        get => Physics2D.OverlapCircle(_groundCheck.position, _groundCheckRadius, _whatIsGround);
    }

    public bool TouchingWall
    {
        get => Physics2D.Raycast(_wallCheck.position, Vector2.right * core.Movement.FacingDirection, _wallCheckDistance, _whatIsGround);
    }

    public bool TouchingWallBack
    {
        get => Physics2D.Raycast(_wallCheck.position, Vector2.right * -core.Movement.FacingDirection, _wallCheckDistance, _whatIsGround);
    }

    public bool TouchingLedge
    {
        get => Physics2D.Raycast(_ledgeCheck.position, Vector2.right * core.Movement.FacingDirection, _wallCheckDistance, _whatIsGround);
    }
    #endregion
}
