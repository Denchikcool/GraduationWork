using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestDummy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private GameObject _hitParticles;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Damage(float damage)
    {
        Debug.Log($"{damage} taken");

        Instantiate(_hitParticles, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        _animator.SetTrigger("damage");
        Destroy(gameObject);
    }
}
