using System;
using UnityEngine;

namespace Denchik.Weapon.Components
{
    [Serializable]
    public abstract class ComponentData
    {
        [SerializeField, HideInInspector]
        private string _name;

        public Type ComponentDependency { get; protected set; }

        public ComponentData()
        {
            SetComponentName();
            SetComponentDependency();
        }

        protected abstract void SetComponentDependency();

        public void SetComponentName()
        {
            _name = GetType().Name;
        }

        public virtual void SetAttackDataNames() { }

        public virtual void InitializeAttackData(int numberOfAttacks) { }
    }

    [Serializable]
    public abstract class ComponentData<T> : ComponentData where T : AttackData
    {
        [SerializeField]
        private T[] _attackData;
        
        [field: SerializeField]
        public T[] AttackData { 
            get
            {
                return _attackData;
            }
            private set 
            { 
                _attackData = value;
            }
        }

        public override void SetAttackDataNames()
        {
            base.SetAttackDataNames();

            for(int i = 0; i < AttackData.Length; i++)
            {
                AttackData[i].SetAttackName(i + 1);
            }
        }

        public override void InitializeAttackData(int numberOfAttacks)
        {
            base.InitializeAttackData(numberOfAttacks);

            int oldLength = _attackData != null ? _attackData.Length : 0;

            if(oldLength == numberOfAttacks)
            {
                return;
            }

            Array.Resize(ref _attackData, numberOfAttacks);

            if(oldLength < numberOfAttacks)
            {
                for(int i = oldLength; i < _attackData.Length; i++)
                {
                    T newObject = Activator.CreateInstance(typeof(T)) as T;
                    _attackData[i] = newObject;
                }
            }

            SetAttackDataNames();
        }
    }
}
