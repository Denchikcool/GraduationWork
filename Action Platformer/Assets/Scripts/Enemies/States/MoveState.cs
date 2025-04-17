using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected DataMoveState dataMoveState;

    protected bool isDetectedWall;
    protected bool isDetectedLedge;
    public MoveState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, DataMoveState dataMoveState) : base(entity, stateMachine, animatorBoolName)
    {
        this.dataMoveState = dataMoveState;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(dataMoveState.MovementSpeed);

        isDetectedLedge = entity.CheckLedge();
        isDetectedWall = entity.CheckWall();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        isDetectedLedge = entity.CheckLedge();
        isDetectedWall = entity.CheckWall();
    }
}
