using UnityEngine;

public class RangeAttackState : AttackState
{
    protected DataRangeAttackState dataRangeAttackState;

    protected GameObject rangeAttackItem;
    protected RangeAttackItem rangeAttackScript;
    public RangeAttackState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, Transform attackPosition, DataRangeAttackState dataRangeAttackState) : base(entity, stateMachine, animatorBoolName, attackPosition)
    {
        this.dataRangeAttackState = dataRangeAttackState;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void MakeChecks()
    {
        base.MakeChecks();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        rangeAttackItem = GameObject.Instantiate(dataRangeAttackState.RangeAttackItem, attackPosition.position, attackPosition.rotation);
        rangeAttackScript = rangeAttackItem.GetComponent<RangeAttackItem>();
        rangeAttackScript.FireRangeAttackItem(dataRangeAttackState.RangeAttackItemSpeed, dataRangeAttackState.RangeAttackItemTravelDistance, dataRangeAttackState.RangeAttackItemDamage);
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
