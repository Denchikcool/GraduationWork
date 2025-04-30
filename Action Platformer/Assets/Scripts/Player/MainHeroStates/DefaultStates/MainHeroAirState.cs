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
    private bool _isTouchingLedge;
    private bool _dashInput;

    private float _startWallJumpCoyoteTime;

    private int _xInput;

    private CollisionSenses _collisionSenses;
    private Movement _movement;

    protected Movement Movement
    {
        get => _movement ?? core.GetCoreComponent(ref _movement);
    }
    private CollisionSenses CollisionSenses
    {
        get => _collisionSenses ?? core.GetCoreComponent(ref _collisionSenses);
    }

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

        _oldIsTouchingWall = false;
        _oldIsTouchingWallBack = false;
        _isTouchingWall = false;
        _isTouchingWallBack = false;
    }

    public override void MakeChecks()
    {
        base.MakeChecks();

        _oldIsTouchingWall = _isTouchingWall;
        _oldIsTouchingWallBack = _isTouchingWallBack;

        if (CollisionSenses)
        {
            _isGrounded = CollisionSenses.TouchingGround;
            _isTouchingWall = CollisionSenses.TouchingWall;
            _isTouchingWallBack = CollisionSenses.TouchingWallBack;
            _isTouchingLedge = CollisionSenses.LedgeHorizontal;
        }

        if(_isTouchingWall && !_isTouchingLedge)
        {
            mainHero.MainHeroLedgeClimbState.SetDetectedPosition(mainHero.transform.position);
        }

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
        _dashInput = mainHero.PlayerInputHandler.DashInput;

        CheckJumpMultiplier();

        if (mainHero.PlayerInputHandler.AttackInput[(int)CombatInput.primary])
        {
            stateMachine.ChangeState(mainHero.PrimaryAttackState);
        }
        else if (mainHero.PlayerInputHandler.AttackInput[(int)CombatInput.secondary])
        {
            stateMachine.ChangeState(mainHero.SecondaryAttackState);
        }
        else if (_isGrounded && Movement?.CurrentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(mainHero.MainHeroLandState);
        }
        else if (_isTouchingWall && !_isTouchingLedge && !_isGrounded)
        {
            stateMachine.ChangeState(mainHero.MainHeroLedgeClimbState);
        }
        else if (_jumpInput && (_isTouchingWall || _isTouchingWallBack || _wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            _isTouchingWall = CollisionSenses.TouchingWall;
            mainHero.MainHeroWallJumpState.DetermineWallJumpDirection(_isTouchingWall);
            stateMachine.ChangeState(mainHero.MainHeroWallJumpState);
        }
        else if(_jumpInput && mainHero.MainHeroJumpState.CanJump())
        {
            stateMachine.ChangeState(mainHero.MainHeroJumpState);
        }
        else if (_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            stateMachine.ChangeState(mainHero.MainHeroWallGrabState);
        }
        else if (_isTouchingWall && _xInput == Movement?.FacingDirection && Movement?.CurrentVelocity.y <= 0)
        {
            stateMachine.ChangeState(mainHero.MainHeroWallSlideState);
        }
        else if(_dashInput && mainHero.MainHeroDashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(mainHero.MainHeroDashState);
        }
        else
        {
            Movement?.CheckShouldFlip(_xInput);
            Movement?.SetHorizontalVelocity(mainHeroData.MovementVelocity * _xInput);

            mainHero.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y);
            mainHero.Animator.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));
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
                Movement?.SetVerticalVelocity(Movement.CurrentVelocity.y * mainHeroData.JumpHeightMultiplier);
                _isJumping = false;
            }
            else if (Movement.CurrentVelocity.y <= 0f)
            {
                _isJumping = false;
            }
        }
    }
}
