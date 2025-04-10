using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _playerRigidBody;
    private Animator _playerAnimation;
    [SerializeField]
    private LayerMask WhatIsGround;

    private int _amountOfJumps;
    private float _movementInputDirection;
    private bool _isFacingRight = true;
    private bool _isWalking;
    private bool _isGrounded;
    private bool _canJump;
    [SerializeField]
    private float GroundCheckRadius;

    public Transform groundCheck;

    public int AmountOfJumps = 1;
    public float MovementSpeed = 8.0f;
    public float JumpForce = 16.0f;
    
    private void Start()
    {
        _playerRigidBody = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<Animator>();
        _amountOfJumps = AmountOfJumps;
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, GroundCheckRadius, WhatIsGround);
    }

    private void CheckMovementDirection()
    {
        if(_isFacingRight && _movementInputDirection < 0) FlipPlayer();
        else if(!_isFacingRight && _movementInputDirection > 0) FlipPlayer();

        //if (_playerRigidBody.velocity.x != 0) _isWalking = true;
        //else _isWalking = false;
        _isWalking = Mathf.Abs(_movementInputDirection) > 0.1f;
    }

    private void UpdateAnimations()
    {
        _playerAnimation.SetBool("isWalking", _isWalking);
        _playerAnimation.SetBool("isGrounded", _isGrounded);
        _playerAnimation.SetFloat("yVelocity", _playerRigidBody.velocity.y);
    }

    private void CheckInput()
    {
        _movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            JumpPlayer();
        }
    }

    private void ApplyMovement()
    {
        _playerRigidBody.velocity = new Vector2(MovementSpeed * _movementInputDirection, _playerRigidBody.velocity.y);
    }

    private void FlipPlayer()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void JumpPlayer()
    {
        if(_canJump) _playerRigidBody.velocity = new Vector2(_playerRigidBody.velocity.x, JumpForce);
        _amountOfJumps--;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, GroundCheckRadius);
    }

    private void CheckIfCanJump()
    {
        if (_isGrounded && _playerRigidBody.velocity.y <= 0) _amountOfJumps = AmountOfJumps;

        if (_amountOfJumps <= 0) _canJump = false;
        else _canJump = true;
    }
}
