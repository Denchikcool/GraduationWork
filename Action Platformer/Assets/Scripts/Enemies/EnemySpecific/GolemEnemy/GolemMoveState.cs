using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemMoveState : MoveState
{
    private Golem _golem;
    public GolemMoveState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataMoveState dataMoveState, Golem golem) : base(entity, stateMachine, animatorBoolName, dataMoveState)
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

        if(isDetectedWall || !isDetectedLedge)
        {
            _golem.IdleState.SetFlipAfterIdle(true);
            stateMachine.ChangeState(_golem.IdleState);
        }
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}
