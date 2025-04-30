using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroCrouchIdleState : MainHeroGroundedState
{
    public MainHeroCrouchIdleState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Movement?.SetVelocityZero();
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
            if(inputXPosition != 0)
            {
                stateMachine.ChangeState(mainHero.MainHeroCrouchMoveState);
            }
            else if (inputYPosition != -1 && !isHeadTouchingWall)
            {
                stateMachine.ChangeState(mainHero.MainHeroIdleState);
            }
        }
    }
}
