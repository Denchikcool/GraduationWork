using UnityEngine;

namespace Denchik.CoreSystem
{
    public class Combat : CoreComponent, IDamageable, IKnockbackable
    {
        [SerializeField]
        private float _maxKnockbackTime = 0.2f;

        [SerializeField]
        private GameObject _damageParticles;

        private bool _isKnockbackActive;

        private float _knockbackStartTime;

        private Movement _movement;
        private CollisionSenses _collisionSenses;
        private Stats _stats;
        private ParticleManager _particleManager;

        private Movement Movement => _movement ? _movement : core.GetCoreComponent(ref _movement);

        private CollisionSenses CollisionSenses => _collisionSenses ? _collisionSenses : core.GetCoreComponent(ref _collisionSenses);

        private Stats Stats => _stats ? _stats : core.GetCoreComponent(ref _stats);

        private ParticleManager ParticleManager => _particleManager ? _particleManager : core.GetCoreComponent(ref _particleManager);

        public override void UpdateLogic()
        {
            CheckKnockback();
        }

        public void Damage(float damage)
        {
            Debug.Log($"{core.transform.parent.name} damaged");
            Stats?.DecreaseHealth(damage);
            ParticleManager?.StartParticlesWithRandomRotation(_damageParticles);
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
            if (_isKnockbackActive && ((Movement?.CurrentVelocity.y <= 0.01f && CollisionSenses.TouchingGround) || Time.time >= _knockbackStartTime + _maxKnockbackTime))
            {
                _isKnockbackActive = false;
                Movement.CanSetVelocity = true;
            }
        }
    }
}
