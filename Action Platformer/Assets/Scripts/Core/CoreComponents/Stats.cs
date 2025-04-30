using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : CoreComponent
{
    [SerializeField]
    private float _maxHealth;

    private float _currentHealth;

    protected override void Awake()
    {
        base.Awake();

        _currentHealth = _maxHealth;
    }

    public void DecreaseHealth(float damage)
    {
        _currentHealth -= damage;

        if(_currentHealth <= 0)
        {
            _currentHealth = 0;
            Debug.Log("Enemy dead!");
        }
    }

    public void IncreaseHealth(float amount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);
    }
}
