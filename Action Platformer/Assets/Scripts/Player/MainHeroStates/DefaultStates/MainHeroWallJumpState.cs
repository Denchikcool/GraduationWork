using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroWallJumpState : MainHeroAbilityState
{
    private int _wallJumpDirection;
    public MainHeroWallJumpState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        mainHero.PlayerInputHandler.ChangeJumpInput();
        mainHero.MainHeroJumpState.ResetAmountOfJumpsLeft();
        Movement?.SetVelocity(mainHeroData.WallJumpVelocity, mainHeroData.WallJumpAngle, _wallJumpDirection);
        Movement?.CheckShouldFlip(_wallJumpDirection);
        mainHero.MainHeroJumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        mainHero.Animator.SetFloat("yVelocity", Movement.CurrentVelocity.y);
        mainHero.Animator.SetFloat("xVelocity", Mathf.Abs(Movement.CurrentVelocity.x));

        if(Time.time >= startTime + mainHeroData.WallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            _wallJumpDirection = -Movement.FacingDirection;
        }
        else
        {
            _wallJumpDirection = Movement.FacingDirection;
        }
    }
}
