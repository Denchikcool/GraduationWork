using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AggressiveWeapon : Weapon
{
    private List<IDamageable> _detectedDamageables = new List<IDamageable>();

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
    }

    public void RemoveFromDetected(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();

        if (damageable != null)
        {
            _detectedDamageables.Remove(damageable);
        }
    }

    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = _aggressiveWeaponData.AttackDetails[attackCounter];

        foreach (IDamageable damageable in _detectedDamageables.ToList())
        {
            damageable.Damage(details.DamageAmount);
        }
    }
}
