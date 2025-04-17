using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemIdleState : IdleState
{
    private Golem _golem;
    public GolemIdleState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataIdleState dataIdleState, Golem golem) : base(entity, stateMachine, animatorBoolName, dataIdleState)
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

        if (isIdleTimeOver)
        {
            stateMachine.ChangeState(_golem.MoveState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
