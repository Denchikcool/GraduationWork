using Denchik.Interfaces;
using Denchik.ObjectPoolSystem;
using System.Collections;
using UnityEngine;

namespace Denchik.ProjectileSystem.Components
{
    public class ReturnToPoolTester : ProjectileComponent, IObjectPoolItem
    {
        private ObjectPool<Projectile> _objectPool;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Init()
        {
            base.Init();

            StartCoroutine(ReturnToPool());
        }

        private IEnumerator ReturnToPool()
        {
            yield return new WaitForSeconds(1f);
            _objectPool.ReturnObject(projectile);
        }

        public void SetObjectPool<T>(ObjectPool<T> pool) where T : Component
        {
            _objectPool = pool as ObjectPool<Projectile>;
        }
    }
}
