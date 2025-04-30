using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    private bool _isKnockbackActive;

    private float _knockbackStartTime;

    public void UpdateLogic()
    {
        CheckKnockback();
    }

    public void Damage(float damage)
    {
        Debug.Log($"{core.transform.parent.name} damaged");
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        core.Movement.SetVelocity(strength, angle, direction);
        core.Movement.CanSetVelocity = false;
        _isKnockbackActive = true;
        _knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if(_isKnockbackActive && core.Movement.CurrentVelocity.y <= 0.01f && core.CollisionSenses.TouchingGround)
        {
            _isKnockbackActive = false;
            core.Movement.CanSetVelocity = true;
        }
    }
}
