using Denchik.Weapon.Components.ComponentData.AttackData;
using UnityEngine;

namespace Denchik.Weapon.Components.ComponentData
{
    public class MovementData : ComponentData
    {
        [field: SerializeField]
        public AttackMovement[] AttackMovementData { get; private set; } 
    }
}
