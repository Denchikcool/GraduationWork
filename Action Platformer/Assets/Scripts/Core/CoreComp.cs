using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Denchik.CoreSystem
{
    public class CoreComp<T> where T : CoreComponent
    {
        private Core _core;
        private T _component;

        public T Component
        {
            get => _component ? _component : _core.GetCoreComponent(ref _component);
        }

        public CoreComp(Core core)
        {
            if(core == null)
            {
                Debug.LogWarning($"core is null for component {typeof(T)}");
            }

            this._core = core;
        }
    }
}
