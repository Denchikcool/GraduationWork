using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroCrouchMoveState : MainHeroGroundedState
{
    public MainHeroCrouchMoveState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        mainHero.SetColliderHeight(mainHeroData.CrouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();

        mainHero.SetColliderHeight(mainHeroData.StandColliderHeight);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (!isExitingState)
        {
            core.Movement.SetHorizontalVelocity(mainHeroData.CrouchMovementVelocity * core.Movement.FacingDirection);
            core.Movement.CheckShouldFlip(inputXPosition);

            if(inputXPosition == 0)
            {
                stateMachine.ChangeState(mainHero.MainHeroCrouchIdleState);
            }
            else if(inputYPosition != -1 && !isHeadTouchingWall)
            {
                stateMachine.ChangeState(mainHero.MainHeroMoveState);
            }
        }
    }
}
