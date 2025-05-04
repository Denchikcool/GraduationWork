using System;
using UnityEngine;

namespace Denchik.Weapon.Components
{
    [Serializable]
    public class AttackDamage : AttackData
    {
        [field: SerializeField]
        public float DamageAmount { get; private set; }
    }
}
