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

        // Создаём и запускаем стрелу (визуальная часть)
        rangeAttackItem = GameObject.Instantiate(dataRangeAttackState.RangeAttackItem, attackPosition.position, attackPosition.rotation);
        rangeAttackScript = rangeAttackItem.GetComponent<RangeAttackItem>();
        rangeAttackScript.FireRangeAttackItem(dataRangeAttackState.RangeAttackItemSpeed, dataRangeAttackState.RangeAttackItemTravelDistance);

        // Логика нанесения урона и отталкивания — здесь, а не в RangeAttackItem
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, dataRangeAttackState.DamageRadius, dataRangeAttackState.WhatIsPlayer);
        Debug.Log($"RangeAttackState: Найдено объектов в радиусе урона: {detectedObjects.Length}");
        foreach (Collider2D collider in detectedObjects)
        {
            Debug.Log($"Проверяем объект: {collider.gameObject.name}");
            if (collider.TryGetComponent(out IDamageable damageable))
            {
                Debug.Log($"IDamageable найден на {collider.gameObject.name}, наносим урон: {dataRangeAttackState.RangeAttackItemDamage}");
                damageable.Damage(dataRangeAttackState.RangeAttackItemDamage);
            }
            else
            {
                Debug.Log($"IDamageable НЕ найден на {collider.gameObject.name}");
            }

            if (collider.TryGetComponent(out IKnockbackable knockbackable))
            {
                // Формируем вектор отталкивания так же, как в ближней атаке
                Vector2 knockbackDirection = new Vector2(
                    Mathf.Cos(dataRangeAttackState.KnockbackAngle * Mathf.Deg2Rad) * Movement.FacingDirection,
                    Mathf.Sin(dataRangeAttackState.KnockbackAngle * Mathf.Deg2Rad)
                );
                Debug.Log($"IKnockbackable найден на {collider.gameObject.name}, применяем отталкивание: направление {knockbackDirection}, сила {dataRangeAttackState.KnockbackStrength}");
                knockbackable.Knockback(knockbackDirection, dataRangeAttackState.KnockbackStrength, Movement.FacingDirection);
            }
            else
            {
                Debug.Log($"IKnockbackable НЕ найден на {collider.gameObject.name}");
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