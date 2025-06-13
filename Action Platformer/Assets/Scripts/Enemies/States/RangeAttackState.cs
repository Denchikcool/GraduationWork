using Denchik.CoreSystem;
using Denchik.Interfaces;
using UnityEngine;

public class RangeAttackState : AttackState
{
    protected DataRangeAttackState dataRangeAttackState;

    protected GameObject rangeAttackItem;
    protected RangeAttackItem rangeAttackScript;

    private Movement _movement;

    public Movement Movement
    {
        get => _movement ? _movement : core.GetCoreComponent(ref _movement);
    }
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
        rangeAttackScript.OnDamageHit += HandleDamageHit;
        rangeAttackScript.FireRangeAttackItem(dataRangeAttackState.RangeAttackItemSpeed, dataRangeAttackState.RangeAttackItemTravelDistance);
    }

    private void HandleDamageHit(GameObject combatObject)
    {
        if (combatObject != null)
        {
            IDamageable damageable = combatObject.GetComponent<IDamageable>();
            if (damageable == null) damageable = combatObject.GetComponentInParent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(dataRangeAttackState.RangeAttackItemDamage);
            }

            IKnockbackable knockbackable = combatObject.GetComponent<IKnockbackable>();
            if (knockbackable == null) knockbackable = combatObject.GetComponentInParent<IKnockbackable>();

            if (knockbackable != null)
            {
                knockbackable.Knockback(dataRangeAttackState.KnockbackAngle, dataRangeAttackState.KnockbackStrength, Movement.FacingDirection);
            }
        }
        else
        {
            Debug.LogWarning("No Combat object received in HandleDamageHit.");
        }
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