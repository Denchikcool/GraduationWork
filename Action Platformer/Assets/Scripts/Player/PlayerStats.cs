using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;

    [SerializeField]
    private GameObject _deathChunkParticle;
    [SerializeField]
    private GameObject _deathBloodParticle;

    private float _currentHealth;

    private GameManager _gameManager;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void DecreaseHealth(float amount)
    {
        _currentHealth -= amount;

        if(_currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(_deathChunkParticle, transform.position, _deathChunkParticle.transform.rotation);
        Instantiate(_deathBloodParticle, transform.position, _deathBloodParticle.transform.rotation);
        _gameManager.Respawn();
        Destroy(gameObject);
    }
}
