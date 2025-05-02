using System;
using UnityEngine;

namespace Denchik.CoreSystem
{
    public class Stats : CoreComponent
    {
        [SerializeField]
        private float _maxHealth;

        private float _currentHealth;

        public event Action OnHealthZero;

        protected override void Awake()
        {
            base.Awake();

            _currentHealth = _maxHealth;
        }

        public void DecreaseHealth(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                _currentHealth = 0;
                OnHealthZero?.Invoke();
            }
        }

        public void IncreaseHealth(float amount)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
        }
    }
}
