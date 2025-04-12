using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask _whatIsGround;
    [SerializeField]
    private float _groundCheckRadius;

    private Rigidbody2D _playerRigidBody;
    private Animator _playerAnimation;
    
    private int _amountOfJumps;
    private int _facingDirection = 1;
    private int _lastWallJumpDirection;

    private float _movementInputDirection;
    private float _jumpTimer;
    private float _turnTimer;
    private float _wallJumpTimer;
    private float _dashTimeLeft;
    private float _lastImageXPosition;
    private float _lastDash = -50.0f;

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
    private bool _isTouchingLedge;
    private bool _canClimbLedge = false;
    private bool _ledgeDetected;
    private bool _isDashing;

    private Vector2 _ledgePositionBottom;
    private Vector2 _ledgePosition1;
    private Vector2 _ledgePosition2;

    public Transform GroundCheck;
    public Transform WallCheck;
    public Transform LedgeCheck;
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
    public float LedgeClimbXOffSet1 = 0.0f;
    public float LedgeClimbYOffSet1 = 0.0f;
    public float LedgeClimbXOffSet2 = 0.0f;
    public float LedgeClimbYOffSet2 = 0.0f;
    public float DashTime;
    public float DashSpeed;
    public float DistanceBetweenImages;
    public float DashCoolDown;


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
        CheckLedgeClimb();
        CheckDash();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(GroundCheck.position, _groundCheckRadius, _whatIsGround);
        _isTouchingWall = Physics2D.Raycast(WallCheck.position, transform.right, WallCheckDistance, _whatIsGround);
        _isTouchingLedge = Physics2D.Raycast(LedgeCheck.position, transform.right, WallCheckDistance, _whatIsGround);

        if(_isTouchingWall && !_isTouchingLedge && !_ledgeDetected)
        {
            _ledgeDetected = true;
            _ledgePositionBottom = WallCheck.position;
        }
    }

    private void CheckMovementDirection()
    {
        if(_isFacingRight && _movementInputDirection < 0) 
            FlipPlayer();
        else if(!_isFacingRight && _movementInputDirection > 0) 
            FlipPlayer();

        if (Mathf.Abs(_playerRigidBody.velocity.x) >= 0.01f) _isWalking = true;
        else _isWalking = false;
        //_isWalking = Mathf.Abs(_movementInputDirection) > 0.1f;
    }

    private void UpdateAnimations()
    {
        _playerAnimation.SetBool("isWalking", _isWalking);
        _playerAnimation.SetBool("isGrounded", _isGrounded);
        _playerAnimation.SetFloat("yVelocity", _playerRigidBody.velocity.y);
        _playerAnimation.SetBool("isWallSliding", _isWallSliding);

        Debug.Log($"isWalking: {_isWalking}, isGrounded: {_isGrounded}, yVelocity: {_playerRigidBody.velocity.y}, isWallSliding: {_isWallSliding}");
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

        if (_turnTimer >= 0)
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

        if (Input.GetButtonDown("Dash"))
        {
            if(Time.time >= (_lastDash + DashCoolDown))
            {
                AttemptToDash();
            }
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
        Gizmos.DrawWireSphere(GroundCheck.position, _groundCheckRadius);
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
        if(_isTouchingWall && _movementInputDirection == _facingDirection && _playerRigidBody.velocity.y < 0 && !_canClimbLedge) 
            _isWallSliding = true;
        else 
            _isWallSliding = false;
    }

    private void CheckLedgeClimb()
    {
        if(_ledgeDetected && !_canClimbLedge)
        {
            _canClimbLedge = true;

            if (_isFacingRight)
            {
                _ledgePosition1 = new Vector2(Mathf.Floor(_ledgePositionBottom.x + WallCheckDistance) - LedgeClimbXOffSet1, Mathf.Floor(_ledgePositionBottom.y) + LedgeClimbYOffSet1);
                _ledgePosition2 = new Vector2(Mathf.Floor(_ledgePositionBottom.x + WallCheckDistance) + LedgeClimbXOffSet2, Mathf.Floor(_ledgePositionBottom.y) + LedgeClimbYOffSet2);
            }
            else
            {
                _ledgePosition1 = new Vector2(Mathf.Ceil(_ledgePositionBottom.x - WallCheckDistance) + LedgeClimbXOffSet1, Mathf.Floor(_ledgePositionBottom.y) + LedgeClimbYOffSet1);
                _ledgePosition2 = new Vector2(Mathf.Ceil(_ledgePositionBottom.x - WallCheckDistance) - LedgeClimbXOffSet2, Mathf.Floor(_ledgePositionBottom.y) +LedgeClimbYOffSet2);
            }
            _canMove = false;
            _canFlip = false;

            _playerAnimation.SetBool("canClimbLedge", _canClimbLedge);
        }

        if (_canClimbLedge)
        {
            transform.position = _ledgePosition1;
        }
    }

    private void AttemptToDash()
    {
        _isDashing = true;
        _dashTimeLeft = DashTime;
        _lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        _lastImageXPosition = transform.position.x;
    }

    private void CheckDash()
    {
        if (_isDashing)
        {
            if (_dashTimeLeft > 0)
            {
                _canMove = false;
                _canFlip = false;
                _playerRigidBody.velocity = new Vector2(DashSpeed * _facingDirection, 0.0f);
                _dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - _lastImageXPosition) > DistanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    _lastImageXPosition = transform.position.x;
                }
            }

            if(_dashTimeLeft <= 0 || _isTouchingWall)
            {
                _isDashing = false;
                _canMove = true;
                _canFlip = true;
            }
        }
    }

    public void FinishLedgeClimb()
    {
        _canClimbLedge = false;
        transform.position = _ledgePosition2;
        _canMove = true;
        _canFlip = true;
        _ledgeDetected = false;
        _playerAnimation.SetBool("canClimbLedge", _canClimbLedge);
    }

    public void DisableFlip()
    {
        _canFlip = false;
    }

    public void EnableFlip()
    {
        _canFlip = true;
    }

    public int GetFacingDirection()
    {
        return _facingDirection;
    }
}
