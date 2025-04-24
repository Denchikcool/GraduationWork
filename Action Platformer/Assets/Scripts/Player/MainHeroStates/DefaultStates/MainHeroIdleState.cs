using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHeroIdleState : MainHeroGroundedState
{
    public MainHeroIdleState(MainHero mainHero, MainHeroStateMachine stateMachine, MainHeroData mainHeroData, string animationBoolName) : base(mainHero, stateMachine, mainHeroData, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        mainHero.SetHorizontalVelocity(0.0f);
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

        if(inputXPosition != 0)
        {
            stateMachine.ChangeState(mainHero.MainHeroMoveState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
