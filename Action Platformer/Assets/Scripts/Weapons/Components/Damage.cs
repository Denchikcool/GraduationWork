using UnityEngine;

namespace Denchik.Weapon.Components
{
    public class Damage : WeaponComponent<DamageData, AttackDamage>
    {
        private ActionHitBox _hitBox;

        protected override void Start()
        {
            base.Start();

            _hitBox = GetComponent<ActionHitBox>();

            _hitBox.OnDetectedCollider += HandleDetectedCollider;
        }

        private void HandleDetectedCollider(Collider2D[] colliders)
        {
            foreach(Collider2D collider in colliders)
            {
                if(collider.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage(currentAttackData.DamageAmount);
                }
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _hitBox.OnDetectedCollider -= HandleDetectedCollider;
        }
    }
}
