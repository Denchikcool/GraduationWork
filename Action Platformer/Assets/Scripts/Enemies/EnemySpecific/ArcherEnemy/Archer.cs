using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Entity
{
    public ArcherIdleState IdleState { get; private set; }
    public ArcherMoveState MoveState { get; private set; }
    public ArcherMainHeroDetectedState MainHeroDetectedState { get; private set; }
    public ArcherMeleeAttackState MeleeAttackState { get; private set; }
    public ArcherFindMainHeroState FindMainHeroState { get; private set; }
    public ArcherStunState StunState { get; private set; }
    public ArcherDeadState DeadState { get; private set; }
    public ArcherDodgeState DodgeState { get; private set; }
    public ArcherRangeAttackState RangeAttackState { get; private set; }

    [SerializeField]
    private DataIdleState _idleStateData;
    [SerializeField]
    private DataMoveState _moveStateData;
    [SerializeField]
    private DataMainHeroDetected _mainHeroDetectedData;
    [SerializeField]
    private DataMeleeAttack _meleeAttackStateData;
    [SerializeField]
    private DataFindMainHero _findMainHeroStateData;
    [SerializeField]
    private DataStunState _stunStateData;
    [SerializeField]
    private DataDeadState _deadStateData;
    [SerializeField]
    public DataDodgeState DodgeStateData;
    [SerializeField]
    private DataRangeAttackState _rangeAttackStateData;

    [SerializeField]
    private Transform _meleeAttackPosition;
    [SerializeField]
    private Transform _rangeAttackPosition;

    public override void Awake()
    {
        base.Awake();

        IdleState = new ArcherIdleState(this, FinalStateMachine, "idle", _idleStateData, this);
        MoveState = new ArcherMoveState(this, FinalStateMachine, "move", _moveStateData, this);
        MainHeroDetectedState = new ArcherMainHeroDetectedState(this, FinalStateMachine, "mainHeroDetected", _mainHeroDetectedData, this);
        MeleeAttackState = new ArcherMeleeAttackState(this, FinalStateMachine, "meleeAttack", _meleeAttackPosition, _meleeAttackStateData, this);
        FindMainHeroState = new ArcherFindMainHeroState(this, FinalStateMachine, "findMainHero", _findMainHeroStateData, this);
        StunState = new ArcherStunState(this, FinalStateMachine, "stun", _stunStateData, this);
        DeadState = new ArcherDeadState(this, FinalStateMachine, "dead", _deadStateData, this);
        DodgeState = new ArcherDodgeState(this, FinalStateMachine, "dodge", DodgeStateData, this);
        RangeAttackState = new ArcherRangeAttackState(this, FinalStateMachine, "rangeAttack", _rangeAttackPosition, _rangeAttackStateData, this);
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
}
