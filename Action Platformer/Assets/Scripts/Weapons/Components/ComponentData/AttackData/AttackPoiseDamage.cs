using System;
using UnityEngine;

namespace Denchik.Weapon.Components
{
    [Serializable]
    public class AttackPoiseDamage : AttackData
    {
        [field : SerializeField]
        public float PoiseAmount { get; private set; }
    }
}
