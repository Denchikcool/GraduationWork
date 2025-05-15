using Denchik.ObjectPoolSystem;
using Denchik.ProjectileSystem.DataPackages;
using UnityEngine;

namespace Denchik.ProjectileSystem
{
    public class ProjectileTester : MonoBehaviour
    {
        public Projectile ProjectilePrefab;

        public DamageDataPackage DamageDataPackage;

        public float ShotCooldown;

        private ObjectPool _objectPool;
        private ObjectPool<Projectile> _pool;

        private float _lastFireTime;

        private void Awake()
        {
            _objectPool = FindObjectOfType<ObjectPool>();
        }

        private void Start()
        {
            if(!ProjectilePrefab)
            {
                Debug.LogWarning("No projectile");
                return;
            }

            _pool = _objectPool.GetPool(ProjectilePrefab);

            FireProjectile();
        }

        private void Update()
        {
            if(Time.time >= _lastFireTime + ShotCooldown)
            {
                FireProjectile();
            }
        }

        private void FireProjectile()
        {
            var projectile = _pool.GetObject();
            projectile.Reset();
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;

            projectile.SendDataPackage(DamageDataPackage);
            projectile.Init();

            _lastFireTime = Time.time;
        }
    }
}
