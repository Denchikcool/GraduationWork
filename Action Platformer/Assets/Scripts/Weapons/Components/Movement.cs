using Denchik.Weapon.Components;
using UnityEngine;

namespace Denchik.Weapon.Components
{
    public class Movement : WeaponComponent<MovementData, AttackMovement>
    {
        private CoreSystem.Movement _coreMovement;

        private CoreSystem.Movement CoreMovement => _coreMovement ? _coreMovement : core.GetCoreComponent(ref _coreMovement);

        protected override void Start()
        {
            base.Start();

            eventHandler.OnStartMovement += HandleStartMovement;
            eventHandler.OnStopMovement += HandleStopMovement;
        }

        private void HandleStartMovement()
        {
            CoreMovement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, CoreMovement.FacingDirection);
        }

        private void HandleStopMovement()
        {
            CoreMovement.SetVelocityZero();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            eventHandler.OnStartMovement -= HandleStartMovement;
            eventHandler.OnStopMovement -= HandleStopMovement;
        }
    }
}
