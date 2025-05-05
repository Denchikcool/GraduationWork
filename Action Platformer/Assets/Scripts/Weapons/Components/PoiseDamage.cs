using Denchik.Interfaces;
using UnityEngine;

namespace Denchik.Weapon.Components
{
    public class PoiseDamage : WeaponComponent<PoiseDamageData, AttackPoiseDamage>
    {
        private ActionHitBox _hitBox;

        protected override void Start()
        {
            base.Start();

            _hitBox = GetComponent<ActionHitBox>();

            _hitBox.OnDetectedCollider += HandleDetectCollider;
        }

        private void HandleDetectCollider(Collider2D[] colliders)
        {
            foreach(Collider2D collider in colliders)
            {
                if (collider.TryGetComponent(out IPoiseDamageable poiseDamageable))
                {
                    poiseDamageable.DamagePoise(currentAttackData.PoiseAmount);
                }
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _hitBox.OnDetectedCollider -= HandleDetectCollider;
        }
    }
}
