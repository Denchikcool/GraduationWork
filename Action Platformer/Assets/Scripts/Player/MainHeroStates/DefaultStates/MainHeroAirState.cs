using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroAirState : MainHeroState
{
    private bool _isGrounded;
    private bool _jumpInput;
    private bool _coyoteTime;
    private bool _isJumping;
    private bool _jumpInputStop;
    private bool _isTouchingWall;
    private bool _grabInput;
    private bool _isTouchingWallBack;
    private bool _wallJumpCoyoteTime;
    private bool _oldIsTouchingWall;
    private bool _oldIsTouchingWallBack;

    private float _startWallJumpCoyoteTime;

    private int _xInput;
    public MainHeroAirState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();

        _oldIsTouchingWall = _isTouchingWall;
        _oldIsTouchingWallBack = _isTouchingWallBack;
        _isGrounded = mainHero.CheckIfTouchingGround();
        _isTouchingWall = mainHero.CheckIfTouchingWall();
        _isTouchingWallBack = mainHero.CheckIfTouchingWallBack();

        if(!_wallJumpCoyoteTime && !_isTouchingWall && !_isTouchingWallBack && (_oldIsTouchingWall || _oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        _xInput = mainHero.PlayerInputHandler.NormalizeInputX;
        _jumpInput = mainHero.PlayerInputHandler.JumpInput;
        _jumpInputStop = mainHero.PlayerInputHandler.JumpInputStop;
        _grabInput = mainHero.PlayerInputHandler.GrabInput;

        CheckJumpMultiplier();

        if (_isGrounded && mainHero.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(mainHero.MainHeroLandState);
        }
        else if (_jumpInput && (_isTouchingWall || _isTouchingWallBack || _wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            _isTouchingWall = mainHero.CheckIfTouchingWall();
            mainHero.MainHeroWallJumpState.DetermineWallJumpDirection(_isTouchingWall);
            stateMachine.ChangeState(mainHero.MainHeroWallJumpState);
        }
        else if(_jumpInput && mainHero.MainHeroJumpState.CanJump())
        {
            stateMachine.ChangeState(mainHero.MainHeroJumpState);
        }
        else if (_isTouchingWall && _grabInput)
        {
            stateMachine.ChangeState(mainHero.MainHeroWallGrabState);
        }
        else if (_isTouchingWall && _xInput == mainHero.FacingDirection && mainHero.CurrentVelocity.y <= 0)
        {
            stateMachine.ChangeState(mainHero.MainHeroWallSlideState);
        }
        else
        {
            mainHero.CheckShouldFlip(_xInput);
            mainHero.SetHorizontalVelocity(mainHeroData.MovementVelocity * _xInput);

            mainHero.Animator.SetFloat("yVelocity", mainHero.CurrentVelocity.y);
            mainHero.Animator.SetFloat("xVelocity", Mathf.Abs(mainHero.CurrentVelocity.x));
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public void StartCoyoteTime()
    {
        _coyoteTime = true;
    }

    public void SetIsJumping()
    {
        _isJumping = true;
    }

    public void StartWallJumpCoyoteTime()
    {
        _wallJumpCoyoteTime = true;
        _startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime()
    {
        _wallJumpCoyoteTime = false;
    }

    private void CheckCoyoteTime()
    {
        if(_coyoteTime && Time.time > _startWallJumpCoyoteTime + mainHeroData.CoyoteTime)
        {
            _coyoteTime = false;
            mainHero.MainHeroJumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if(_wallJumpCoyoteTime && Time.time > startTime + mainHeroData.CoyoteTime)
        {
            _wallJumpCoyoteTime = false;
        }
    }

    private void CheckJumpMultiplier()
    {
        if (_isJumping)
        {
            if (_jumpInputStop)
            {
                mainHero.SetVerticalVelocity(mainHero.CurrentVelocity.y * mainHeroData.JumpHeightMultiplier);
                _isJumping = false;
            }
            else if (mainHero.CurrentVelocity.y <= 0f)
            {
                _isJumping = false;
            }
        }
    }
}
