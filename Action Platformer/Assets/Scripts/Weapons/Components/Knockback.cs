using Denchik.CoreSystem;
using Denchik.Interfaces;
using UnityEngine;

namespace Denchik.Weapon.Components
{
    public class Knockback : WeaponComponent<KnockbackData, AttackKnokback>
    {
        private ActionHitBox _hitBox;

        private CoreSystem.Movement _movement;

        protected override void Start()
        {
            base.Start();

            _hitBox = GetComponent<ActionHitBox>();

            _hitBox.OnDetectedCollider += HandleDetectedCollider;

            _movement = core.GetCoreComponent<CoreSystem.Movement>();
        }

        private void HandleDetectedCollider(Collider2D[] colliders)
        {
            foreach(Collider2D collider in colliders)
            {
                if(collider.TryGetComponent(out IKnockbackable knockbackable))
                {
                    knockbackable.Knockback(currentAttackData.KnockbackAngle, currentAttackData.KnockbackStrength, _movement.FacingDirection);
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
