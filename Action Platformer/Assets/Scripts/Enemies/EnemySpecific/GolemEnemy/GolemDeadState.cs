using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDeadState : DeadState
{
    private Golem _golem;
    public GolemDeadState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataDeadState dataDeadState, Golem golem) : base(entity, stateMachine, animatorBoolName, dataDeadState)
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
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
