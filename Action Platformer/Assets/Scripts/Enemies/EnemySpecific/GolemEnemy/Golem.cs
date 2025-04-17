using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Entity
{
    public GolemIdleState IdleState { get; private set; }
    public GolemMoveState MoveState { get; private set; }
    public GolemMainHeroDetectedState MainHeroDetectedState { get; private set; }

    [SerializeField]
    private DataIdleState _idleStateData;
    [SerializeField]
    private DataMoveState _moveStateData;
    [SerializeField]
    private DataMainHeroDetected _mainHeroDetectedData;

    public override void Start()
    {
        base.Start();

        MoveState = new GolemMoveState(this, FinalStateMachine, "move", _moveStateData, this);
        IdleState = new GolemIdleState(this, FinalStateMachine, "idle", _idleStateData, this);
        MainHeroDetectedState = new GolemMainHeroDetectedState(this, FinalStateMachine, "mainHeroDetected", _mainHeroDetectedData, this);

        FinalStateMachine.Initialize(MoveState);
    }
}
