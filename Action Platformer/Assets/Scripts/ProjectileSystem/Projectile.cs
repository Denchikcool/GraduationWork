using Denchik.ProjectileSystem.DataPackages;
using System;
using UnityEngine;

namespace Denchik.ProjectileSystem
{
    public class Projectile : MonoBehaviour
    {
        public event Action OnInit;
        public event Action OnReset;
        public event Action<ProjectileDataPackage> OnReceiveDataPackage;

        public Rigidbody2D Rigidbody2D { get; private set; }

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            OnInit?.Invoke();
        }

        public void Reset()
        {
            OnReset?.Invoke();
        }

        public void SendDataPackage(ProjectileDataPackage dataPackage)
        {
            OnReceiveDataPackage?.Invoke(dataPackage);
        }
    }
}
