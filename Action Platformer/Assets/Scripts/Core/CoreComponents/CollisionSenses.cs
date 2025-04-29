using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    #region Transforms
    public Transform GroundCheck 
    {   get 
        {
            if (_groundCheck)
            {
                return _groundCheck;
            }
            Debug.LogError("No ground check on " + core.transform.parent.name);
            return null;
        }
        private set => _groundCheck = value; 
    }
    public Transform WallCheck 
    {
        get
        {
            if (_wallCheck)
            {
                return _wallCheck;
            }
            Debug.LogError("No wall check on " + core.transform.parent.name);
            return null;
        } 
        private set => _wallCheck = value; 
    }
    public Transform LedgeCheckHorizontal 
    {   get
        {
            if (_ledgeCheckHorizontal)
            {
                return _ledgeCheckHorizontal;
            }
            Debug.LogError("No ledge check horizontal on " + core.transform.parent.name);
            return null;
        }
        private set => _ledgeCheckHorizontal = value; 
    }
    public Transform LedgeCheckVertical 
    {   get
        {
            if(_ledgeCheckVertical)
            {
                return _ledgeCheckVertical;
            }
            Debug.LogError("No ledge check vertical on " + core.transform.parent.name);
            return null;
        }
        private set => _ledgeCheckVertical = value; 
    }
    public Transform UpHeadCheck 
    {   get
        {
            if (_upHeadCheck)
            {
                return _upHeadCheck;
            }
            Debug.LogError("No upHead check on " + core.transform.parent.name);
            return null;
        } 
        private set => _upHeadCheck = value; 
    }

    public float GroundCheckRadius { get => _groundCheckRadius; set => _groundCheckRadius = value; }
    public float WallCheckDistance { get => _wallCheckDistance; set => _wallCheckDistance = value; }
    public LayerMask WhatIsGround { get => _whatIsGround; set => _whatIsGround = value; }

    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private Transform _wallCheck;
    [SerializeField]
    private Transform _ledgeCheckHorizontal;
    [SerializeField]
    private Transform _ledgeCheckVertical;
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
        get => Physics2D.OverlapCircle(UpHeadCheck.position, _groundCheckRadius, _whatIsGround);
    }

    public bool TouchingGround
    {
        get => Physics2D.OverlapCircle(GroundCheck.position, _groundCheckRadius, _whatIsGround);
    }

    public bool TouchingWall
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * core.Movement.FacingDirection, _wallCheckDistance, _whatIsGround);
    }

    public bool TouchingWallBack
    {
        get => Physics2D.Raycast(WallCheck.position, Vector2.right * -core.Movement.FacingDirection, _wallCheckDistance, _whatIsGround);
    }

    public bool LedgeHorizontal
    {
        get => Physics2D.Raycast(LedgeCheckHorizontal.position, Vector2.right * core.Movement.FacingDirection, _wallCheckDistance, _whatIsGround);
    }

    public bool LedgeVertical
    {
        get => Physics2D.Raycast(LedgeCheckVertical.position, Vector2.down, _wallCheckDistance, _whatIsGround);
    }
    #endregion
}
