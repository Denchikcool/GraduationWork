using System;
using UnityEngine;

namespace Denchik.ProjectileSystem.DataPackages
{
    [Serializable]
    public class DamageDataPackage : ProjectileDataPackage
    {
        [field: SerializeField]
        public float DamageAmount { get; private set; }
    }
}
