using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Entity
{
    public GolemIdleState IdleState { get; private set; }
    public GolemMoveState MoveState { get; private set; }

    [SerializeField]
    private DataIdleState _idleStateData;
    [SerializeField]
    private DataMoveState _moveStateData;

    public override void Start()
    {
        base.Start();

        MoveState = new GolemMoveState(this, FinalStateMachine, "move", _moveStateData, this);
        IdleState = new GolemIdleState(this, FinalStateMachine, "idle", _idleStateData, this);

        FinalStateMachine.Initialize(MoveState);
    }
}
