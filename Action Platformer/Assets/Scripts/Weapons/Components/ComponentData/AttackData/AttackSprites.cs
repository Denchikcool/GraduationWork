using System;
using UnityEngine;

namespace Denchik.Weapon.Components.ComponentData.AttackData
{
    [Serializable]
    public class AttackSprites
    {
        [field: SerializeField]
        public Sprite[] Sprites { get; private set; }
    }
}
