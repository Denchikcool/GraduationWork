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
    public GolemMeleeAttackState MeleeAttackState { get; private set; }
    public GolemStunState StunState { get; private set; }
    public GolemDeadState DeadState { get; private set; }

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
    [SerializeField]
    private DataMeleeAttack _meleeAttackStateData;
    [SerializeField]
    private DataStunState _stunStateData;
    [SerializeField]
    private DataDeadState _deadStateData;

    [SerializeField]
    private Transform _meleeAttackPosition;

    public override void Awake()
    {
        base.Awake();

        MoveState = new GolemMoveState(this, FinalStateMachine, "move", _moveStateData, this);
        IdleState = new GolemIdleState(this, FinalStateMachine, "idle", _idleStateData, this);
        MainHeroDetectedState = new GolemMainHeroDetectedState(this, FinalStateMachine, "mainHeroDetected", _mainHeroDetectedData, this);
        ChargeState = new GolemChargeState(this, FinalStateMachine, "charge", _chargeStateData, this);
        FindMainHeroState = new GolemFindMainHeroState(this, FinalStateMachine, "findMainHero", _findMainHeroStateData, this);
        MeleeAttackState = new GolemMeleeAttackState(this, FinalStateMachine, "meleeAttack", _meleeAttackPosition, _meleeAttackStateData, this);
        StunState = new GolemStunState(this, FinalStateMachine, "stun", _stunStateData, this);
        DeadState = new GolemDeadState(this, FinalStateMachine, "dead", _deadStateData, this);
    }

    private void Start()
    {
        FinalStateMachine.Initialize(MoveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(_meleeAttackPosition.position, _meleeAttackStateData.AttackRadius);
    }

    public override void TakeDamage(AttackDetails details)
    {
        base.TakeDamage(details);

        if (isDead)
        {
            FinalStateMachine.ChangeState(DeadState);
        }
        else if (isStunned && FinalStateMachine.CurrentState != StunState)
        {
            FinalStateMachine.ChangeState(StunState);
        }
    }
}
