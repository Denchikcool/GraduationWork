using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Entity
{
    public GolemIdleState IdleState { get; private set; }
    public GolemMoveState MoveState { get; private set; }
    public GolemMainHeroDetectedState MainHeroDetectedState { get; private set; }
    public GolemChargeState ChargeState { get; private set; }
    public GolemFindMainHeroState FindMainHeroState { get; private set; }

    [SerializeField]
    private DataIdleState _idleStateData;
    [SerializeField]
    private DataMoveState _moveStateData;
    [SerializeField]
    private DataMainHeroDetected _mainHeroDetectedData;
    [SerializeField]
    private DataChargeState _chargeStateData;
    [SerializeField]
    private DataFindMainHero _findMainHeroStateData;

    public override void Start()
    {
        base.Start();

        MoveState = new GolemMoveState(this, FinalStateMachine, "move", _moveStateData, this);
        IdleState = new GolemIdleState(this, FinalStateMachine, "idle", _idleStateData, this);
        MainHeroDetectedState = new GolemMainHeroDetectedState(this, FinalStateMachine, "mainHeroDetected", _mainHeroDetectedData, this);
        ChargeState = new GolemChargeState(this, FinalStateMachine, "charge", _chargeStateData, this);
        FindMainHeroState = new GolemFindMainHeroState(this, FinalStateMachine, "findMainHero", _findMainHeroStateData, this);

        FinalStateMachine.Initialize(MoveState);
    }
}
