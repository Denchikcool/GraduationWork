using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroGroundedState : MainHeroState
{
    protected int inputXPosition;

    private bool _jumpInput;
    private bool _isGrounded;

    public MainHeroGroundedState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        mainHero.MainHeroJumpState.ResetAmountOfJumpsLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();

        _isGrounded = mainHero.CheckIfTouchingGround();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        inputXPosition = mainHero.PlayerInputHandler.NormalizeInputX;
        _jumpInput = mainHero.PlayerInputHandler.JumpInput;

        if (_jumpInput && mainHero.MainHeroJumpState.CanJump())
        {
            mainHero.PlayerInputHandler.ChangeJumpInput();
            stateMachine.ChangeState(mainHero.MainHeroJumpState);
        }
        else if (!_isGrounded)
        {
            mainHero.MainHeroAirState.StartCoyoteTime();
            stateMachine.ChangeState(mainHero.MainHeroAirState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
