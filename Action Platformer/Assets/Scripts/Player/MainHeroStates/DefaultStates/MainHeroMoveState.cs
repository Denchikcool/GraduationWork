using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroMoveState : MainHeroGroundedState
{
    public MainHeroMoveState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
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
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        mainHero.CheckShouldFlip(inputXPosition);
        mainHero.SetHorizontalVelocity(mainHeroData.MovementVelocity * inputXPosition);

        if(inputXPosition == 0 && !isExitingState)
        {
            stateMachine.ChangeState(mainHero.MainHeroIdleState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
