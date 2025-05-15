using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Denchik.Interfaces
{
    public interface IObjectPoolItem
    {
        public void SetObjectPool<T>(ObjectPool<T> pool) where T : Component;
    }
}
