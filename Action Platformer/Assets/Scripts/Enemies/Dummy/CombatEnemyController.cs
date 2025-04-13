using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatEnemyController : MonoBehaviour
{
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _knockbackSpeedX;
    [SerializeField]
    private float _knockbackSpeedY;
    [SerializeField]
    private float _knockbackDeathSpeedX;
    [SerializeField]
    private float _knockbackDeathSpeedY;
    [SerializeField]
    private float _deathTorque;
    [SerializeField]
    private float _knockbackDuration;
    [SerializeField]
    private bool _applyKnockback;
    [SerializeField]
    private GameObject _hitParticle;

    private float _currentHealth;
    private float _knockbackStart;

    private int _playerFacingDirection;

    private bool _playerOnLeft;
    private bool _knockback;

    private PlayerController _playerController;
    private GameObject _aliveGameObject;
    private GameObject _brokenTopGameObject;
    private GameObject _brokenBottomGameObject;
    private Rigidbody2D _rigidBodyAlive;
    private Rigidbody2D _rigidBodyBrokenTop;
    private Rigidbody2D _rigidBodyBrokenBottom;
    private Animator _aliveAnimator;

    private void Start()
    {
        _currentHealth = _maxHealth;

        _playerController = GameObject.Find("MainHero").GetComponent<PlayerController>();
        _aliveGameObject = transform.Find("Alive").gameObject;
        _brokenTopGameObject = transform.Find("BrokenTop").gameObject;
        _brokenBottomGameObject = transform.Find("BrokenBottom").gameObject;

        _aliveAnimator = _aliveGameObject.GetComponent<Animator>();
        _rigidBodyAlive = _aliveGameObject.GetComponent<Rigidbody2D>();
        _rigidBodyBrokenTop = _brokenTopGameObject.GetComponent<Rigidbody2D>();
        _rigidBodyBrokenBottom = _brokenBottomGameObject.GetComponent<Rigidbody2D>();

        _aliveGameObject.SetActive(true);
        _brokenTopGameObject.SetActive(false);
        _brokenBottomGameObject.SetActive(false);
    }

    private void Update()
    {
        CheckKnockback();
    }

    private void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        _playerFacingDirection = _playerController.GetFacingDirection();

        Instantiate(_hitParticle, _aliveGameObject.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (_playerFacingDirection == 1)
            _playerOnLeft = true;
        else
            _playerOnLeft = false;

        _aliveAnimator.SetBool("playerOnLeft", _playerOnLeft);
        _aliveAnimator.SetTrigger("damage");

        if (_applyKnockback && _currentHealth > 0.0f)
            Knockback();

        if (_currentHealth <= 0.0f)
            Die();
    }

    private void Knockback()
    {
        _knockback = true;
        _knockbackStart = Time.time;
        _rigidBodyAlive.velocity = new Vector2(_knockbackSpeedX * _playerFacingDirection, _knockbackSpeedY);
    }

    private void CheckKnockback()
    {
        if((Time.time >= (_knockbackStart + _knockbackDuration)) && _knockback)
        {
            _knockback = false;
            _rigidBodyAlive.velocity = new Vector2(0.0f, _rigidBodyAlive.velocity.y);
        }
    }

    private void Die()
    {
        _aliveGameObject.SetActive(false);
        _brokenTopGameObject.SetActive(true);
        _brokenBottomGameObject.SetActive(true);

        _brokenTopGameObject.transform.position = _aliveGameObject.transform.position;
        _brokenBottomGameObject.transform.position = _aliveGameObject.transform.position;

        _rigidBodyBrokenTop.velocity = new Vector2(_knockbackDeathSpeedX * _playerFacingDirection, _knockbackDeathSpeedY);
        _rigidBodyBrokenBottom.velocity = new Vector2(_knockbackSpeedX * _playerFacingDirection, _knockbackSpeedY);
        _rigidBodyBrokenTop.AddTorque(_deathTorque * -_playerFacingDirection, ForceMode2D.Impulse);
    }
}
