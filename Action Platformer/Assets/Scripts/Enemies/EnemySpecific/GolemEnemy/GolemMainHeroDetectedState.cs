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

        if (!isMainHeroInMaxAgroRange)
        {
            _golem.IdleState.SetFlipAfterIdle(false);
            stateMachine.ChangeState(_golem.IdleState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
