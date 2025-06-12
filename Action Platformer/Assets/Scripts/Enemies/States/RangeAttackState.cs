using Denchik.Interfaces;
using UnityEngine;
using Denchik.CoreSystem;

public class RangeAttackState : AttackState
{
    protected DataRangeAttackState dataRangeAttackState;

    protected GameObject rangeAttackItem;
    protected RangeAttackItem rangeAttackScript;

    private Movement _movement;

    private Movement Movement
    {
        get => _movement ?? core.GetCoreComponent(ref _movement);
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
        rangeAttackScript.FireRangeAttackItem(dataRangeAttackState.RangeAttackItemSpeed, dataRangeAttackState.RangeAttackItemTravelDistance);
    }
    /*public override void TriggerAttack()
    {
        base.TriggerAttack();

        // ������ � ��������� ������ (���������� �����)
        rangeAttackItem = GameObject.Instantiate(dataRangeAttackState.RangeAttackItem, attackPosition.position, attackPosition.rotation);
        rangeAttackScript = rangeAttackItem.GetComponent<RangeAttackItem>();
        rangeAttackScript.FireRangeAttackItem(dataRangeAttackState.RangeAttackItemSpeed, dataRangeAttackState.RangeAttackItemTravelDistance);

        // ������ ��������� ����� � ������������ � �����, � �� � RangeAttackItem
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, dataRangeAttackState.DamageRadius, dataRangeAttackState.WhatIsPlayer);
        Debug.Log($"RangeAttackState: ������� �������� � ������� �����: {detectedObjects.Length}");
        foreach (Collider2D collider in detectedObjects)
        {
            Debug.Log($"��������� ������: {collider.gameObject.name}");
            if (collider.TryGetComponent(out IDamageable damageable))
            {
                Debug.Log($"IDamageable ������ �� {collider.gameObject.name}, ������� ����: {dataRangeAttackState.RangeAttackItemDamage}");
                damageable.Damage(dataRangeAttackState.RangeAttackItemDamage);
            }
            else
            {
                Debug.Log($"IDamageable �� ������ �� {collider.gameObject.name}");
            }

            if (collider.TryGetComponent(out IKnockbackable knockbackable))
            {
                // ��������� ������ ������������ ��� ��, ��� � ������� �����
                Vector2 knockbackDirection = new Vector2(
                    Mathf.Cos(dataRangeAttackState.KnockbackAngle * Mathf.Deg2Rad) * Movement.FacingDirection,
                    Mathf.Sin(dataRangeAttackState.KnockbackAngle * Mathf.Deg2Rad)
                );
                Debug.Log($"IKnockbackable ������ �� {collider.gameObject.name}, ��������� ������������: ����������� {knockbackDirection}, ���� {dataRangeAttackState.KnockbackStrength}");
                knockbackable.Knockback(knockbackDirection, dataRangeAttackState.KnockbackStrength, Movement.FacingDirection);
            }
            else
            {
                Debug.Log($"IKnockbackable �� ������ �� {collider.gameObject.name}");
            }
        }
    }*/

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }
}