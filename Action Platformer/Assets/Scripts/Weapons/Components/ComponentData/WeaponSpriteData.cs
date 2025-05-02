using Denchik.Weapon.Components.ComponentData.AttackData;
using UnityEngine;

namespace Denchik.Weapon.Components.ComponentData
{
    public class WeaponSpriteData : ComponentData
    {
        [field: SerializeField]
        public AttackSprites[] AttackData {  get; private set; }
    }
}
