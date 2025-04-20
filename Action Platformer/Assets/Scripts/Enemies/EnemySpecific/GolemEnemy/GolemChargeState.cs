using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemChargeState : ChargeState
{
    private Golem _golem;
    public GolemChargeState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataChargeState dataChargeState, Golem golem) : base(entity, stateMachine, animatorBoolName, dataChargeState)
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

        if(!isDetectedLedge || isDetectedWall)
        {
            stateMachine.ChangeState(_golem.FindMainHeroState);
        }
        else if (isChargeTimeOver)
        {
            if(isMainHeroInMinAgroRange)
            {
                stateMachine.ChangeState(_golem.MainHeroDetectedState);
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
