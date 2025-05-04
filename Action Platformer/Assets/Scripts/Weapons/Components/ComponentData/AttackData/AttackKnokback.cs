using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Denchik.Weapon.Components
{
    [Serializable]
    public class AttackKnokback : AttackData
    {
        [field: SerializeField]
        public Vector2 KnockbackAngle { get; private set; }

        [field: SerializeField]
        public float KnockbackStrength { get; private set; }
    }
}
