using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : CoreComponent, IDamageable, IKnockbackable
{
    [SerializeField]
    private float _maxKnockbackTime = 0.2f;

    private bool _isKnockbackActive;

    private float _knockbackStartTime;

    private Movement _movement;
    private CollisionSenses _collisionSenses;
    private Stats _stats;

    private Movement Movement
    {
        get => _movement ?? core.GetCoreComponent(ref _movement);
    }

    private CollisionSenses CollisionSenses
    {
        get => _collisionSenses ?? core.GetCoreComponent(ref _collisionSenses);
    }

    private Stats Stats
    {
        get => _stats ?? core.GetCoreComponent(ref _stats);
    }

    public override void UpdateLogic()
    {
        CheckKnockback();
    }

    public void Damage(float damage)
    {
        Debug.Log($"{core.transform.parent.name} damaged");
        Stats?.DecreaseHealth(damage);
    }

    public void Knockback(Vector2 angle, float strength, int direction)
    {
        Movement?.SetVelocity(strength, angle, direction);
        Movement.CanSetVelocity = false;
        _isKnockbackActive = true;
        _knockbackStartTime = Time.time;
    }

    private void CheckKnockback()
    {
        if(_isKnockbackActive && ((Movement?.CurrentVelocity.y <= 0.01f && CollisionSenses.TouchingGround) || Time.time >= _knockbackStartTime + _maxKnockbackTime))
        {
            _isKnockbackActive = false;
            Movement.CanSetVelocity = true;
        }
    }
}
