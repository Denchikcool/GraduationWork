using Denchik.Interfaces;
using Denchik.ProjectileSystem.DataPackages;
using Denchik.Utilities;
using UnityEngine;

namespace Denchik.ProjectileSystem.Components
{
    public class Damage : ProjectileComponent
    {
        [field: SerializeField]
        public LayerMask LayerMask { get; private set; }

        private float _damageAmount;

        private HitBox _hitBox;

        protected override void Awake()
        {
            base.Awake();

            _hitBox = GetComponent<HitBox>();

            _hitBox.OnRayCastHit2D += HandleRaycastHit2D;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _hitBox.OnRayCastHit2D += HandleRaycastHit2D;
        }

        public void HandleRaycastHit2D(RaycastHit2D[] hits)
        {
            foreach(RaycastHit2D hit in hits)
            {
                if(!LayerMaskUtilities.IsLayerInMask(hit, LayerMask))
                {
                    return;
                }

                if(hit.transform.TryGetComponent(out IDamageable damageable))
                {
                    damageable.Damage(_damageAmount);
                }
            }
        }

        protected override void HandleReceiveDataPackage(ProjectileDataPackage dataPackage)
        {
            base.HandleReceiveDataPackage(dataPackage);

            if(dataPackage is not DamageDataPackage package)
            {
                return;
            }

            _damageAmount = package.DamageAmount;
        }
    }
}
