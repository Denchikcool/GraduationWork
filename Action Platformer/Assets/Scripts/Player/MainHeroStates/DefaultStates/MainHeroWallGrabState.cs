using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroWallGrabState : MainHeroTouchWallState
{
    private Vector2 _holdPosition;
    public MainHeroWallGrabState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
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

        _holdPosition = mainHero.transform.position;

        HoldPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!isExitingState)
        {
            HoldPosition();

            if (yInput > 0)
            {
                stateMachine.ChangeState(mainHero.MainHeroWallClimbState);
            }
            else if (yInput < 0 || !grabInput)
            {
                stateMachine.ChangeState(mainHero.MainHeroWallSlideState);
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    private void HoldPosition()
    {
        mainHero.transform.position = _holdPosition;

        Movement?.SetHorizontalVelocity(0.0f);
        Movement?.SetVerticalVelocity(0.0f);
    }
}
