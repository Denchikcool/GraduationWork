using UnityEngine;

namespace Denchik.Weapon.Components
{
    public class AttackData
    {
        [SerializeField, HideInInspector]
        private string _name;

        public void SetAttackName(int number)
        {
            _name = $"Attack {number}";
        }
    }
}
