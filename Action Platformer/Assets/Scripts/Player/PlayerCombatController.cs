using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField]
    private bool _combatEnabled;
    [SerializeField]
    private float _inputTimer;
    [SerializeField]
    private float _attackRadius;
    [SerializeField]
    private float _attackDamage;
    [SerializeField]
    private Transform _attackHitBoxPosition;
    [SerializeField]
    private LayerMask _whatIsDamageable;

    private bool _gotInput;
    private bool _isAttacking;
    private bool _isFirstAttack;
    
    private float _lastInputTime = Mathf.NegativeInfinity;
    private AttackDetails _attackDetails; 

    private Animator _animator;

    private PlayerController _playerController;
    private PlayerStats _playerStats;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("canAttack", _combatEnabled);
        _playerController = GetComponent<PlayerController>();
        _playerStats = GetComponent<PlayerStats>();
    }
    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_combatEnabled)
            {
                _gotInput = true;
                _lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if (_gotInput)
        {
            if (!_isAttacking)
            {
                _gotInput = false;
                _isAttacking = true;
                _isFirstAttack = !_isFirstAttack;
                _animator.SetBool("attack", true);
                _animator.SetBool("firstAttack", _isFirstAttack);
                _animator.SetBool("isAttacking", _isAttacking);
            }
        }

        if(Time.time >= (_lastInputTime + _inputTimer))
        {
            _gotInput = false;
            //_isAttacking = false;
            //_animator.SetBool("isAttacking", _isAttacking);
            //_animator.SetBool("attack", false);
        }
    }

    private void CheckAttackHitBox()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(_attackHitBoxPosition.position, _attackRadius, _whatIsDamageable);

        _attackDetails.DamageAmount = _attackDamage;
        _attackDetails.Position = transform.position;

        foreach(Collider2D item in detectedObjects)
        {
            item.transform.parent.SendMessage("TakeDamage", _attackDetails);
        }
    }

    private void FinishAttack()
    {
        _isAttacking = false;
        _animator.SetBool("isAttacking", _isAttacking);
        _animator.SetBool("attack", false);
    }

    private void TakeDamage(AttackDetails attackDetails)
    {
        if (!_playerController.GetDashStatus())
        {
            int direction;

            _playerStats.DecreaseHealth(attackDetails.DamageAmount);

            if (attackDetails.Position.x < transform.position.x)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

            _playerController.Knockback(direction);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_attackHitBoxPosition.position, _attackRadius);
    }
}
