using System;
using UnityEngine;

namespace Denchik.Weapon.Components
{
    [Serializable]
    public class AttackSprites : AttackData
    {
        [field: SerializeField]
        public PhaseSprites[] PhaseSprites { get; private set; }
    }

    [Serializable]
    public struct PhaseSprites
    {
        [field: SerializeField]
        public Sprite[] Sprites { get; private set; }

        [field: SerializeField]
        public AttackPhases Phases { get; private set; }
    }
}
