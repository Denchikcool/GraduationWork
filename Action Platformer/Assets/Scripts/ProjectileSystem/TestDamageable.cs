using Denchik.Interfaces;
using UnityEngine;

namespace Denchik.ProjectileSystem
{
    public class TestDamageable : MonoBehaviour, IDamageable
    {
        public void Damage(float damage)
        {
            print($"{gameObject.name} Damaged: {damage}");
        }
    }
}
