using Denchik.ObjectPoolSystem;
using UnityEngine;

namespace Denchik.Interfaces
{
    public interface IObjectPoolItem
    {
        public void SetObjectPool<T>(ObjectPool<T> pool) where T : Component;
    }
}
