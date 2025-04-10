using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRigidBody;
    private Animator _playerAnimation;
    [SerializeField]
    private LayerMask WhatIsGround;

    private int _amountOfJumps;
    private int _facingDirection = 1;
    private float _movementInputDirection;
    private bool _isFacingRight = true;
    private bool _isWalking;
    private bool _isTouchingWall;
    private bool _isWallSliding;
    private bool _isGrounded;
    private bool _canJump;
    [SerializeField]
    private float GroundCheckRadius;

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
            JumpPlayer();

        if (Input.GetButtonUp("Jump"))
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, _playerRigidBody.velocity.y * VariableJumpHeightMultiplier);
        
    }

    private void ApplyMovement()
    {
        if (_isGrounded)
        {
            _playerRigidBody.velocity = new Vector2(MovementSpeed * _movementInputDirection, _playerRigidBody.velocity.y);
        }  
        else if (!_isGrounded && !_isWallSliding && _movementInputDirection != 0)
        {
            Vector2 forceToAdd = new Vector2(MovementForceInAir * _movementInputDirection, 0);
            _playerRigidBody.AddForce(forceToAdd);

            if(Mathf.Abs(_playerRigidBody.velocity.x) > MovementSpeed)
            {
                _playerRigidBody.velocity = new Vector2(MovementSpeed * _movementInputDirection, _playerRigidBody.velocity.y);
            }
        }
        else if(!_isGrounded && !_isWallSliding && _movementInputDirection == 0)
        {
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x * AirDragMultiplier, _playerRigidBody.velocity.y);
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
        if (!_isWallSliding)
        {
            _facingDirection *= -1;
            _isFacingRight = !_isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }

    private void JumpPlayer()
    {
        if (_canJump && !_isWallSliding)
        {
            _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, JumpForce);
            _amountOfJumps--;
        }
        else if (_isWallSliding && _movementInputDirection == 0 && _canJump)
        {
            _isWallSliding = false;
            _amountOfJumps--;
            Vector2 forceToAdd = new Vector2(WallHopForce * WallHopDirection.x * -_facingDirection, WallHopForce * WallHopDirection.y);
            _playerRigidBody.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
        else if((_isWallSliding || _isTouchingWall) && _movementInputDirection != 0 && _canJump)
        {
            _isWallSliding = false;
            _amountOfJumps--;
            Vector2 forceToAdd = new Vector2(WallJumpForce * WallJumpDirection.x * _movementInputDirection, WallJumpForce * WallJumpDirection.y);
            _playerRigidBody.AddForce(forceToAdd, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundCheck.position, GroundCheckRadius);
        Gizmos.DrawLine(WallCheck.position, new Vector3(WallCheck.position.x + WallCheckDistance, WallCheck.position.y, WallCheck.position.z));
    }

    private void CheckIfCanJump()
    {
        if ((_isGrounded && _playerRigidBody.velocity.y <= 0) || _isWallSliding) 
            _amountOfJumps = AmountOfJumps;

        if (_amountOfJumps <= 0) 
            _canJump = false;
        else 
            _canJump = true;
    }

    private void CheckIfWallSliding()
    {
        if(_isTouchingWall && !_isGrounded && _playerRigidBody.velocity.y < 0) 
            _isWallSliding = true;
        else 
            _isWallSliding = false;
    }
}
