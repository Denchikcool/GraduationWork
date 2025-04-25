using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroTouchWallState : MainHeroState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool grabInput;

    protected int xInput;
    protected int yInput;
    public MainHeroTouchWallState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
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

        isGrounded = mainHero.CheckIfTouchingGround();
        isTouchingWall = mainHero.CheckIfTouchingWall();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        xInput = mainHero.PlayerInputHandler.NormalizeInputX;
        yInput = mainHero.PlayerInputHandler.NormalizeInputY;
        grabInput = mainHero.PlayerInputHandler.GrabInput;

        if (isGrounded && !grabInput)
        {
            stateMachine.ChangeState(mainHero.MainHeroIdleState);
        }
        else if(!isTouchingWall || (xInput != mainHero.FacingDirection && !grabInput))
        {
            stateMachine.ChangeState(mainHero.MainHeroAirState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
