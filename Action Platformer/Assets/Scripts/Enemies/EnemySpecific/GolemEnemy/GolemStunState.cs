using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemStunState : StunState
{
    private Golem _golem;
    public GolemStunState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataStunState dataStunState, Golem golem) : base(entity, stateMachine, animatorBoolName, dataStunState)
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

        if (isStunTimeOver)
        {
            if (performCloseRangeAction)
            {
                stateMachine.ChangeState(_golem.MeleeAttackState);
            }
            else if (isMainHeroInMinAgroRange)
            {
                stateMachine.ChangeState(_golem.ChargeState);
            }
            else
            {
                _golem.FindMainHeroState.SetTurnNow(true);
                stateMachine.ChangeState(_golem.FindMainHeroState);
            }
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
