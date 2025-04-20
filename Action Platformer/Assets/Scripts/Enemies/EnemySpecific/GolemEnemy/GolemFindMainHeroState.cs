using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemFindMainHeroState : FindMainHeroState
{
    private Golem _golem;
    public GolemFindMainHeroState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataFindMainHero dataFindMainHero, Golem golem) : base(entity, stateMachine, animatorBoolName, dataFindMainHero)
    {
        this._golem = golem;
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

        if (isMainHeroInMinAgroRange)
        {
            stateMachine.ChangeState(_golem.MainHeroDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(_golem.MoveState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
