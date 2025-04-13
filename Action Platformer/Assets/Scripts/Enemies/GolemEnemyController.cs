using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemEnemyController : MonoBehaviour
{
    private enum State
    {
        Moving,
        Knockback,
        Dead
    }

    private State _currentState;

    private Vector2 _movement;

    private GameObject _alive;
    private Rigidbody2D _aliveRigidBody;
    private Animator _aliveAnimator;

    private bool _groundDetected;
    private bool _wallDetected;

    private int _facingDirection;
    private int _damageDirection;

    private float _currentHealth;
    private float _knockbackStartTime;

    [SerializeField]
    private Transform _groundCheck;
    [SerializeField]
    private Transform _wallCheck;
    [SerializeField]
    private LayerMask _whatIsGround;
    [SerializeField]
    private float _groundCheckDistance;
    [SerializeField]
    private float _wallCheckDistance;
    [SerializeField]
    private float _movementSpeed;
    [SerializeField]
    private float _maxHealth;
    [SerializeField]
    private float _knockbackDuration;
    [SerializeField]
    private Vector2 _knockbackSpeed;
    [SerializeField]
    private GameObject _hitParticle;
    [SerializeField]
    private GameObject _deathChunkParticle;
    [SerializeField]
    private GameObject _deathBloodParticle;

    private void Start()
    {
        _alive = transform.Find("Alive").gameObject;
        _aliveRigidBody = _alive.GetComponent<Rigidbody2D>();
        _aliveAnimator = _alive.GetComponent<Animator>();

        _facingDirection = 1;
        _currentHealth = _maxHealth;
    }
    private void Update()
    {
        switch (_currentState)
        {
            case State.Moving:
                UpdateMovingState();
                break;
            case State.Knockback:
                UpdateKnockbackState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
    }

    private void EnterMovingState()
    {

    }

    private void UpdateMovingState()
    {
        _groundDetected = Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _whatIsGround);
        _wallDetected = Physics2D.Raycast(_wallCheck.position, transform.right, _wallCheckDistance, _whatIsGround);

        if(!_groundDetected || _wallDetected)
        {
            Flip();
        }
        else
        {
            _movement.Set(_movementSpeed * _facingDirection, _aliveRigidBody.velocity.y);
            _aliveRigidBody.velocity = _movement;
        }
    }

    private void ExitMovingState()
    {

    }

    private void EnterKnockbackState()
    {
        _knockbackStartTime = Time.time;
        _movement.Set(_knockbackSpeed.x * _damageDirection, _knockbackSpeed.y);
        _aliveRigidBody.velocity = _movement;
        _aliveAnimator.SetBool("knockback", true);
    }

    private void UpdateKnockbackState()
    {
        if(Time.time >= _knockbackStartTime + _knockbackDuration)
        {
            SwitchState(State.Moving);
        }
    }

    private void ExitKnockbackState()
    {
        _aliveAnimator.SetBool("knockback", false);
    }

    private void EnterDeadState()
    {
        Destroy(gameObject);
    }

    private void UpdateDeadState()
    {

    }

    private void ExitDeadState()
    {

    }

    private void Flip()
    {
        _facingDirection *= -1;
        _alive.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void TakeDamage(float[] attackDetails)
    {
        _currentHealth -= attackDetails[0];

        Instantiate(_hitParticle, _alive.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if (attackDetails[1] > _alive.transform.position.x)
        {
            _damageDirection = -1;
        }
        else
        {
            _damageDirection = 1;
        }

        if(_currentHealth > 0.0f)
        {
            SwitchState(State.Knockback);
        }
        else if(_currentHealth <= 0.0f)
        {
            SwitchState(State.Dead);
        }
    }

    private void SwitchState(State state)
    {
        switch (_currentState)
        {
            case State.Moving:
                ExitMovingState();
                break;
            case State.Knockback:
                ExitKnockbackState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Moving:
                EnterMovingState();
                break;
            case State.Knockback:
                EnterKnockbackState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        _currentState = state;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_groundCheck.position, new Vector2(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance));
        Gizmos.DrawLine(_wallCheck.position, new Vector2(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y));
    }
}
