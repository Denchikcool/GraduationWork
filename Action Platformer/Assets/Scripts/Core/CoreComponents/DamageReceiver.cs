using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Denchik.CoreSystem
{
    public class DamageReceiver : CoreComponent, IDamageable
    {
        [SerializeField]
        private GameObject _damageParticles;

        private CoreComp<Stats> _stats;
        private CoreComp<ParticleManager> _particleManager;

        protected override void Awake()
        {
            base.Awake();

            _stats = new CoreComp<Stats>(core);
            _particleManager = new CoreComp<ParticleManager>(core);
        }

        public void Damage(float damage)
        {
            Debug.Log($"{core.transform.parent.name} damaged");
            _stats.Component?.DecreaseHealth(damage);
            _particleManager.Component?.StartParticlesWithRandomRotation(_damageParticles);
        }
    }
}
