using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Denchik.CoreSystem.StatsSystem
{
    [Serializable]
    public class Stat
    {
        [field: SerializeField]
        public float MaxValue { get; private set; }

        private float _currentValue;

        public event Action OnCurrentValueZero;

        public float CurrentValue
        {
            get
            {
                return _currentValue;
            }
            private set
            {
                _currentValue = Mathf.Clamp(value, 0.0f, MaxValue);

                if(_currentValue <= 0.0f)
                {
                    OnCurrentValueZero?.Invoke();
                }
            }
        }

        public void Init()
        {
            CurrentValue = MaxValue;
        }

        public void IncreaseValue(float value)
        {
            CurrentValue += value;
        }

        public void DecreaseValue(float value)
        {
            CurrentValue -= value;
            SoundEffectManager.PlaySound("Hit");
        }
    }
}
