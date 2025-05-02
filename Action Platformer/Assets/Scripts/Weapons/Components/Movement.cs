using Denchik.Weapon.Components.ComponentData;
using UnityEngine;

namespace Denchik.Weapon.Components
{
    public class Movement : WeaponComponent
    {
        private CoreSystem.Movement _coreMovement;

        private CoreSystem.Movement CoreMovement => _coreMovement ? _coreMovement : core.GetCoreComponent(ref _coreMovement);

        private MovementData _movementData;

        protected override void Awake()
        {
            base.Awake();

            _movementData = weapon.WeaponData.GetData<MovementData>();
        }

        private void HandleStartMovement()
        {
            var currentAttackData = _movementData.AttackMovementData[weapon.CurrentAttackCounter];

            CoreMovement.SetVelocity(currentAttackData.Velocity, currentAttackData.Direction, CoreMovement.FacingDirection);
        }

        private void HandleStopMovement()
        {
            CoreMovement.SetVelocityZero();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            eventHandler.OnStartMovement += HandleStartMovement;
            eventHandler.OnStopMovement += HandleStopMovement;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            eventHandler.OnStartMovement -= HandleStartMovement;
            eventHandler.OnStopMovement -= HandleStopMovement;
        }
    }
}
