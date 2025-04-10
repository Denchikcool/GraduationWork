using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRigidBody;
    private Animator _playerAnimation;
    [SerializeField]
    private LayerMask WhatIsGround;
    [SerializeField]
    private float GroundCheckRadius;

    private int _amountOfJumps;
    private int _facingDirection = 1;
    private int _lastWallJumpDirection;

    private float _movementInputDirection;
    private float _jumpTimer;
    private float _turnTimer;
    private float _wallJumpTimer;

    private bool _isFacingRight = true;
    private bool _isWalking;
    private bool _isTouchingWall;
    private bool _isWallSliding;
    private bool _isGrounded;
    private bool _canNormalJump;
    private bool _canWallJump;
    private bool _isAttemptingToJump;
    private bool _checkJumpMultiplier;
    private bool _canMove;
    private bool _canFlip;
    private bool _hasWallJumped;

    public Transform GroundCheck;
    public Transform WallCheck;
    public Vector2 WallHopDirection;
    public Vector2 WallJumpDirection;

    public int AmountOfJumps = 1;

    public float MovementSpeed = 8.0f;
    public float JumpForce = 16.0f;
    public float WallCheckDistance;
    public float WallSlideSpeed;
    public float MovementForceInAir;
    public float AirDragMultiplier = 0.95f;
    public float VariableJumpHeightMultiplier = 0.5f;
    public float WallHopForce;
    public float WallJumpForce;
    public float JumpTimerSet = 0.15f;
    public float TurnTimerSet = 0.1f;
    public float WallJumpTimerSet = 0.5f;


    private void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<Animator>();
        _amountOfJumps = AmountOfJumps;
        WallHopDirection.Normalize();
        WallJumpDirection.Normalize();
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckJump();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, WhatIsGround);
        _isTouchingWall = Physics2D.Raycast(WallCheck.position, transform.right, WallCheckDistance, WhatIsGround);
    }

    private void CheckMovementDirection()
    {
        if(_isFacingRight && _movementInputDirection < 0) 
            FlipPlayer();
        else if(!_isFacingRight && _movementInputDirection > 0) 
            FlipPlayer();

        //if (_playerRigidBody.velocity.x != 0) _isWalking = true;
        //else _isWalking = false;
        _isWalking = Mathf.Abs(_movementInputDirection) > 0.1f;
    }

    private void UpdateAnimations()
    {
        _playerAnimation.SetBool("isWalking", _isWalking);
        _playerAnimation.SetBool("isGrounded", _isGrounded);
        _playerAnimation.SetFloat("yVelocity", _playerRigidBody.velocity.y);
        _playerAnimation.SetBool("isWallSliding", _isWallSliding);
    }

    private void CheckInput()
    {
        _movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            if(_isGrounded || (_amountOfJumps > 0 && !_isTouchingWall))
            {
                NormalJump();
            }
            else
            {
                _jumpTimer = JumpTimerSet;
                _isAttemptingToJump = true;
            }
        }
            
        if(Input.GetButtonDown("Horizontal") && _isTouchingWall)
        {
            if(!_isGrounded && _movementInputDirection != _facingDirection)
            {
                _canMove = false;
                _canFlip = false;
                _turnTimer = TurnTimerSet;
            }
        }

        if (!_canMove)
        {
            _turnTimer -= Time.deltaTime;

            if(_turnTimer <= 0)
            {
                _canMove = true;
                _canFlip = true;
            }
        }

        if (_checkJumpMultiplier && !Input.GetButton("Jump"))
        {
            _checkJumpMultiplier = false;
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, _playerRigidBody.velocity.y * VariableJumpHeightMultiplier);
        } 
    }

    private void ApplyMovement()
    {
        if (!_isGrounded && !_isWallSliding && _movementInputDirection == 0)
        {
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x * AirDragMultiplier, _playerRigidBody.velocity.y);
        }
        else if(_canMove)
        {
            _playerRigidBody.velocity = new Vector2(MovementSpeed * _movementInputDirection, _playerRigidBody.velocity.y);
        }
       
        if (_isWallSliding)
        {
            if(_playerRigidBody.velocity.y < -WallSlideSpeed)
            {
                _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, -WallSlideSpeed);
            }
        }
    }

    private void FlipPlayer()
    {
        if (!_isWallSliding && _canFlip)
        {
            _facingDirection *= -1;
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void CheckJump()
    {
        if(_jumpTimer > 0)
        {
            if(!_isGrounded && _isTouchingWall && _movementInputDirection != 0 && _movementInputDirection != _facingDirection)
            {
                WallJump();
            }
            else if (_isGrounded)
            {
                NormalJump();
            }
        }

        if(_isAttemptingToJump)
        {
            _jumpTimer -= Time.deltaTime;
        }

        if(_wallJumpTimer > 0)
        {
            if(_hasWallJumped && _movementInputDirection == -_lastWallJumpDirection)
            {
                _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, 0.0f);
                _hasWallJumped = false;
            }
            else if(_wallJumpTimer <= 0)
            {
                _hasWallJumped = false;
            }
            else
            {
                _wallJumpTimer -= Time.deltaTime;
            }
        }
    }

    private void NormalJump()
    {
        if (_canNormalJump)
        {
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, JumpForce);
            _amountOfJumps--;
            _jumpTimer = 0;
            _isAttemptingToJump = false;
            _checkJumpMultiplier = true;
        }
    }

    private void WallJump()
    {
        if (_canWallJump)
        {
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, 0.0f);
            _isWallSliding = false;
            _amountOfJumps = AmountOfJumps;
            _amountOfJumps--;
            Vector2 forceToAdd = new Vector2(WallJumpForce * WallJumpDirection.x * _movementInputDirection, WallJumpForce * WallJumpDirection.y);
            _playerRigidBody.AddForce(forceToAdd, ForceMode2D.Impulse);
            _jumpTimer = 0;
            _isAttemptingToJump = false;
            _checkJumpMultiplier = true;
            _turnTimer = 0;
            _canMove = true;
            _canFlip = true;
            _hasWallJumped = true;
            _wallJumpTimer = WallJumpTimerSet;
            _lastWallJumpDirection = -_facingDirection;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
        Gizmos.DrawLine(WallCheck.position, new Vector3(WallCheck.position.x + WallCheckDistance, WallCheck.position.y, WallCheck.position.z));
    }

    private void CheckIfCanJump()
    {
        if (_isGrounded && _playerRigidBody.velocity.y <= 0.01f) 
            _amountOfJumps = AmountOfJumps;

        if(_isTouchingWall)
            _canWallJump = true;

        if (_amountOfJumps <= 0) 
            _canNormalJump = false;
        else 
            _canNormalJump = true;
    }

    private void CheckIfWallSliding()
    {
        if(_isTouchingWall && _movementInputDirection == _facingDirection && _playerRigidBody.velocity.y < 0) 
            _isWallSliding = true;
        else 
            _isWallSliding = false;
    }
}
