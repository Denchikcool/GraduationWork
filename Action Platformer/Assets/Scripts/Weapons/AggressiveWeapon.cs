using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AggressiveWeapon : Weapon
{
    private List<IDamageable> _detectedDamageables = new List<IDamageable>();
    private List<IKnockbackable> _detectedKnockbackables = new List<IKnockbackable>();

    protected DataAggressiveWeapon _aggressiveWeaponData;

    protected override void Awake()
    {
        base.Awake();

        if(_weaponData.GetType() == typeof(DataAggressiveWeapon))
        {
            _aggressiveWeaponData = (DataAggressiveWeapon)_weaponData;
        }
        else
        {
            Debug.LogError("Wrong data of weapon!");
        }
    }
    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();

        CheckMeleeAttack();
    }

    public void AddToDetected(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();

        if(damageable != null)
        {
            _detectedDamageables.Add(damageable);
        }

        IKnockbackable knockbackable = collider.GetComponent<IKnockbackable>();

        if (knockbackable != null)
        {
            _detectedKnockbackables.Add(knockbackable);
        }
    }

    public void RemoveFromDetected(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();

        if (damageable != null)
        {
            _detectedDamageables.Remove(damageable);
        }

        IKnockbackable knockbackable = collider.GetComponent<IKnockbackable>();

        if (knockbackable != null)
        {
            _detectedKnockbackables.Remove(knockbackable);
        }
    }

    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = _aggressiveWeaponData.AttackDetails[attackCounter];

        foreach (IDamageable damageable in _detectedDamageables.ToList())
        {
            damageable.Damage(details.DamageAmount);
        }

        foreach(IKnockbackable knockbackable in _detectedKnockbackables.ToList())
        {
            knockbackable.Knockback(details.KnockbackAngle, details.KnockbackStrength, core.Movement.FacingDirection);
        }
    }
}
