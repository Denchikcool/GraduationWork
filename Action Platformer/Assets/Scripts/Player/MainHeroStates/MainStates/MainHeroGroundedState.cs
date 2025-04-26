using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroGroundedState : MainHeroState
{
    protected int inputXPosition;

    private bool _jumpInput;
    private bool _isGrounded;
    private bool _isTouchingWall;
    private bool _grabInput;
    private bool _isTouchingLedge;
    private bool _dashInput;

    public MainHeroGroundedState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        mainHero.MainHeroJumpState.ResetAmountOfJumpsLeft();
        mainHero.MainHeroDashState.ResetCanDash();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();

        _isGrounded = mainHero.CheckIfTouchingGround();
        _isTouchingWall = mainHero.CheckIfTouchingWall();
        _isTouchingLedge = mainHero.CheckIfTouchingLedge();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        inputXPosition = mainHero.PlayerInputHandler.NormalizeInputX;
        _jumpInput = mainHero.PlayerInputHandler.JumpInput;
        _grabInput = mainHero.PlayerInputHandler.GrabInput;
        _dashInput = mainHero.PlayerInputHandler.DashInput;

        if (_jumpInput && mainHero.MainHeroJumpState.CanJump())
        {
            stateMachine.ChangeState(mainHero.MainHeroJumpState);
        }
        else if (!_isGrounded)
        {
            mainHero.MainHeroAirState.StartCoyoteTime();
            stateMachine.ChangeState(mainHero.MainHeroAirState);
        }
        else if(_isTouchingWall && _grabInput && _isTouchingLedge)
        {
            stateMachine.ChangeState(mainHero.MainHeroWallGrabState);
        }
        else if (_dashInput && mainHero.MainHeroDashState.CheckIfCanDash())
        {
            stateMachine.ChangeState(mainHero.MainHeroDashState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
