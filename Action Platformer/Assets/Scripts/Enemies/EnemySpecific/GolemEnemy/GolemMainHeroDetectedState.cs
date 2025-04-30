using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMainHeroDetectedState : MainHeroDetectedState
{
    private Golem _golem;
    public GolemMainHeroDetectedState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataMainHeroDetected detectedMainHeroData, Golem golem) : base(entity, stateMachine, animatorBoolName, detectedMainHeroData)
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

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        if (performShortRangeAction)
        {
            stateMachine.ChangeState(_golem.MeleeAttackState);
        }
        else if (performLongRangeAction)
        {
            stateMachine.ChangeState(_golem.ChargeState);
        }
        else if (!isMainHeroInMaxAgroRange)
        {
            stateMachine.ChangeState(_golem.FindMainHeroState);
        }
        else if (!isDetectedLedge)
        {
            Movement?.Flip();
            stateMachine.ChangeState(_golem.MoveState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
