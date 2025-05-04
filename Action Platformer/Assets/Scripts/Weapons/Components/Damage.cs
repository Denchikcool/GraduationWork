using UnityEngine;

namespace Denchik.Weapon.Components
{
    public class Damage : WeaponComponent<DamageData, AttackDamage>
    {
        private ActionHitBox _hitBox;

        protected override void Awake()
        {
            base.Awake();

            _hitBox = GetComponent<ActionHitBox>();
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

        protected override void OnEnable()
        {
            base.OnEnable();

            _hitBox.OnDetectedCollider += HandleDetectedCollider;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _hitBox.OnDetectedCollider -= HandleDetectedCollider;
        }
    }
}
