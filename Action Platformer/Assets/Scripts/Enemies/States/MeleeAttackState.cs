using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected DataMeleeAttack dataMeleeAttack;

    public MeleeAttackState(Entity entity, FinalStateMachine stateMachine, string animatorBoolName, Transform attackPosition, DataMeleeAttack dataMeleeAttack) : base(entity, stateMachine, animatorBoolName, attackPosition)
    {
        this.dataMeleeAttack = dataMeleeAttack;
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

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, dataMeleeAttack.AttackRadius, dataMeleeAttack.WhatIsPlayer);

        foreach(Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if(damageable != null)
            {
                damageable.Damage(dataMeleeAttack.AttackDamage);
            }

            IKnockbackable knockbackable = collider.GetComponent<IKnockbackable>();

            if(knockbackable != null)
            {
                knockbackable.Knockback(dataMeleeAttack.KnockbackAngle, dataMeleeAttack.KnockbackStrength, core.Movement.FacingDirection);
            }
            //collider.transform.SendMessage("TakeDamage", attackDetails);
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
