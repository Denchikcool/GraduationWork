using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Denchik.Interfaces;

namespace Denchik.CoreSystem
{
    public class PoiseDamageReceiver : CoreComponent, IPoiseDamageable
    {
        private Stats _stats;

        protected override void Awake()
        {
            base.Awake();

            _stats = core.GetCoreComponent<Stats>();
        }

        public void DamagePoise(float damagePoise)
        {
            _stats.Poise.DecreaseValue(damagePoise);
        }
    }
}
