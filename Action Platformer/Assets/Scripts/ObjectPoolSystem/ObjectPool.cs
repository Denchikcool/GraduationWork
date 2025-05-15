using Denchik.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Denchik.ObjectPoolSystem
{
    public class ObjectPool
    {
        [field: SerializeField]
        public int StartCount { get; private set; }

        private Dictionary<string, object> _pools = new Dictionary<string, object>();

        public ObjectPool<T> GetPool<T>(T prefab) where T : Component
        {
            if (!_pools.ContainsKey(prefab.name))
            {
                _pools[prefab.name] = new ObjectPool<T>(prefab, StartCount);
            }

            return (ObjectPool<T>)_pools[prefab.name];
        }

        public void ReturnObject<T>(T obj) where T : Component
        {
            var objPool = GetPool(obj);
            objPool.ReturnObject(obj);
        }
    }

    public class ObjectPool<T> where T : Component
    {
        private readonly T _prefab;
        private readonly Queue<T> _poolStack = new Queue<T>();

        public ObjectPool(T prefab, int startCount = 0)
        {
            this._prefab = prefab;

            for(int i = 0; i < startCount; i++)
            {
                var obj = InstantiateNewObject();
                _poolStack.Enqueue(obj);
            }
        }

        private T InstantiateNewObject()
        {
            var obj = Object.Instantiate(_prefab);
            obj.name = _prefab.name;

            var objectPoolItem = obj.GetComponent<IObjectPoolItem>();
            objectPoolItem.SetObjectPool(this);

            return obj;
        }

        public T GetObject()
        {
            if(!_poolStack.TryDequeue(out var obj))
            {
                obj = InstantiateNewObject();
                return obj;
            }

            obj.gameObject.SetActive(true);
            return obj;
        }

        public void ReturnObject(T obj)
        {
            obj.gameObject.SetActive(false);
            _poolStack.Enqueue(obj);
        }
    }
}
