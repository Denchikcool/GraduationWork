using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherFindMainHeroState : FindMainHeroState
{
    private Archer _archer;
    public ArcherFindMainHeroState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataFindMainHero dataFindMainHero, Archer archer) : base(entity, stateMachine, animatorBoolName, dataFindMainHero)
    {
        this._archer = archer;
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
            stateMachine.ChangeState(_archer.MainHeroDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(_archer.MoveState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
