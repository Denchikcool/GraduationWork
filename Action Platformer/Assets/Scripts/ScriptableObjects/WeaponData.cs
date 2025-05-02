using Denchik.Weapon.Components.ComponentData;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Denchik.Weapon
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data")]
    public class WeaponData : ScriptableObject
    {
        [field: SerializeField]
        public int NumberOfAttacks { get; private set; }

        [field: SerializeReference]
        public List<ComponentData> ComponentData { get; private set; }

        [ContextMenu("Add sprite data")]
        private void AddSpriteData()
        {
            ComponentData.Add(new WeaponSpriteData());
        }

        [ContextMenu("Add movement data")]
        private void AddMovementData()
        {
            ComponentData.Add(new MovementData());
        }

        public T GetData<T>()
        {
            return ComponentData.OfType<T>().FirstOrDefault();
        }
    }
}
