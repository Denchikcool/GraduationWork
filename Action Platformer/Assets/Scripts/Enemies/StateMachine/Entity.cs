using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Unity Objects
    [SerializeField]
    private Transform _wallCheck;
    [SerializeField]
    private Transform _ledgeCheck;
    [SerializeField]
    private Transform _playerCheck;
    [SerializeField]
    private Transform _groundCheck;
    #endregion

    #region States
    public FinalStateMachine FinalStateMachine;
    #endregion

    #region Data
    public DataEntity DataEntity;
    #endregion

    #region Variables
    private Vector2 _velocityWorkSpace;

    private float _currentHealth;
    private float _currentStunResistance;
    private float _lastDamageTime;

    protected bool isStunned;
    protected bool isDead;
    #endregion

    #region Components
    public int LastDamageDirection { get; private set; }
    public Animator Animator { get; private set; }
    public AnimationToStateMachine AnimationToStateMachine { get; private set; }
    public Core Core { get; private set; }
    #endregion

    #region Unity Functions
    public virtual void Awake()
    {
        Core = GetComponentInChildren<Core>();

        _currentHealth = DataEntity.MaxHealth;
        _currentStunResistance = DataEntity.StunResistance;

        Animator = GetComponent<Animator>();
        AnimationToStateMachine = GetComponent<AnimationToStateMachine>();

        FinalStateMachine = new FinalStateMachine();
    }

    public virtual void Update()
    {
        FinalStateMachine.CurrentState.UpdateLogic();

        Animator.SetFloat("yVelocity", Core.Movement.Rigidbody.velocity.y);

        if(Time.time >= _lastDamageTime + DataEntity.StunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        FinalStateMachine.CurrentState.UpdatePhysics();
    }
    #endregion

    #region Check Functions
    public virtual bool CheckMainHeroInMinAgroRange()
    {
        return Physics2D.Raycast(_playerCheck.position, transform.right, DataEntity.MinAgroDistance, DataEntity.WhatIsPlayer);
    }

    public virtual bool CheckMainHeroInMaxAgroRange()
    {
        return Physics2D.Raycast(_playerCheck.position, transform.right, DataEntity.MaxAgroDistance, DataEntity.WhatIsPlayer);
    }

    public virtual bool CheckMainHeroInCloseRangeAction()
    {
        return Physics2D.Raycast(_playerCheck.position, transform.right, DataEntity.CloseRangeActionDistance, DataEntity.WhatIsPlayer);
    }
    #endregion

    #region Damage Functions
    public virtual void TakeDamage(AttackDetails details)
    {
        _lastDamageTime = Time.time;
        _currentHealth -= details.DamageAmount;
        _currentStunResistance -= details.StunDamageAmount;

        DamageHop(DataEntity.DamageHopSpeed);

        Instantiate(DataEntity.HitParticle, transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if(details.Position.x > transform.position.x)
        {
            LastDamageDirection = -1;
        }
        else
        {
            LastDamageDirection = 1;
        }

        if(_currentStunResistance <= 0)
        {
            isStunned = true;
        }

        if(_currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public virtual void DamageHop(float velocity)
    {
        _velocityWorkSpace.Set(Core.Movement.Rigidbody.velocity.x, velocity);
        Core.Movement.Rigidbody.velocity = _velocityWorkSpace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        _currentStunResistance = DataEntity.StunResistance;
    }
    #endregion

    #region Gizmos Function
    public virtual void OnDrawGizmos()
    {
        if (Core != null)
        {
            Gizmos.DrawLine(_wallCheck.position, _wallCheck.position + (Vector3)(Vector2.right * Core.Movement.FacingDirection * DataEntity.WallCheckDistance));
            Gizmos.DrawLine(_ledgeCheck.position, _ledgeCheck.position + (Vector3)(Vector2.down * DataEntity.LedgeCheckDistance));
            Gizmos.DrawWireSphere(_playerCheck.position + (Vector3)(Vector2.right * DataEntity.CloseRangeActionDistance), 0.2f);
            Gizmos.DrawWireSphere(_playerCheck.position + (Vector3)(Vector2.right * DataEntity.MinAgroDistance), 0.2f);
            Gizmos.DrawWireSphere(_playerCheck.position + (Vector3)(Vector2.right * DataEntity.MaxAgroDistance), 0.2f);
        }
    }
    #endregion
}
