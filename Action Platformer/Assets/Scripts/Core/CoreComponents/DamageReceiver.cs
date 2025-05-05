using Denchik.Interfaces;
using UnityEngine;

namespace Denchik.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField]
        private GameObject _damageParticles;

        private Stats _stats;
        private ParticleManager _particleManager;

        protected override void Awake()
        {
            base.Awake();

            _stats = core.GetCoreComponent<Stats>();
            _particleManager = core.GetCoreComponent<ParticleManager>();
        }

        public void Damage(float damage)
        {
            Debug.Log($"{core.transform.parent.name} damaged");
            _stats.Health.DecreaseValue(damage);
            _particleManager.StartParticlesWithRandomRotation(_damageParticles);
        }
    }
}
