using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    private Transform _wallCheck;
    [SerializeField]
    private Transform _ledgeCheck;
    [SerializeField]
    private Transform _playerCheck;
    [SerializeField]
    private Transform _groundCheck;

    public FinalStateMachine FinalStateMachine;

    public DataEntity DataEntity;

    private Vector2 _velocityWorkSpace;

    private float _currentHealth;
    private float _currentStunResistance;
    private float _lastDamageTime;

    protected bool isStunned;
    protected bool isDead;

    public int FacingDirection { get; private set; }
    public int LastDamageDirection { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public GameObject AliveGameObject { get; private set; }
    public AnimationToStateMachine AnimationToStateMachine { get; private set; }

    public virtual void Start()
    {
        FacingDirection = 1;
        _currentHealth = DataEntity.MaxHealth;
        _currentStunResistance = DataEntity.StunResistance;

        AliveGameObject = transform.Find("Alive").gameObject;
        Rigidbody = AliveGameObject.GetComponent<Rigidbody2D>();
        Animator = AliveGameObject.GetComponent<Animator>();
        AnimationToStateMachine = AliveGameObject.GetComponent<AnimationToStateMachine>();

        FinalStateMachine = new FinalStateMachine();
    }

    public virtual void Update()
    {
        FinalStateMachine.CurrentState.UpdateLogic();

        if(Time.time >= _lastDamageTime + DataEntity.StunRecoveryTime)
        {
            ResetStunResistance();
        }
    }

    public virtual void FixedUpdate()
    {
        FinalStateMachine.CurrentState.UpdatePhysics();
    }

    public virtual void SetVelocity(float velocity)
    {
        _velocityWorkSpace.Set(FacingDirection * velocity, Rigidbody.velocity.y);
        Rigidbody.velocity = _velocityWorkSpace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        _velocityWorkSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        Rigidbody.velocity = _velocityWorkSpace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(_wallCheck.position, AliveGameObject.transform.right, DataEntity.WallCheckDistance, DataEntity.WhatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(_ledgeCheck.position, Vector2.down, DataEntity.LedgeCheckDistance, DataEntity.WhatIsGround);
    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, DataEntity.GroundCheckRadius, DataEntity.WhatIsGround);
    }

    public virtual void Flip()
    {
        FacingDirection *= -1;
        AliveGameObject.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public virtual void TakeDamage(AttackDetails details)
    {
        _lastDamageTime = Time.time;
        _currentHealth -= details.DamageAmount;
        _currentStunResistance -= details.StunDamageAmount;

        DamageHop(DataEntity.DamageHopSpeed);

        Instantiate(DataEntity.HitParticle, AliveGameObject.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

        if(details.Position.x > AliveGameObject.transform.position.x)
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
        _velocityWorkSpace.Set(Rigidbody.velocity.x, velocity);
        Rigidbody.velocity = _velocityWorkSpace;
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        _currentStunResistance = DataEntity.StunResistance;
    }

    public virtual bool CheckMainHeroInMinAgroRange()
    {
        return Physics2D.Raycast(_playerCheck.position, AliveGameObject.transform.right, DataEntity.MinAgroDistance, DataEntity.WhatIsPlayer);
    }

    public virtual bool CheckMainHeroInMaxAgroRange()
    {
        return Physics2D.Raycast(_playerCheck.position, AliveGameObject.transform.right, DataEntity.MaxAgroDistance, DataEntity.WhatIsPlayer);
    }

    public virtual bool CheckMainHeroInCloseRangeAction()
    {
        return Physics2D.Raycast(_playerCheck.position, AliveGameObject.transform.right, DataEntity.CloseRangeActionDistance, DataEntity.WhatIsPlayer);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(_wallCheck.position, _wallCheck.position + (Vector3)(Vector2.right * FacingDirection * DataEntity.WallCheckDistance));
        Gizmos.DrawLine(_ledgeCheck.position, _ledgeCheck.position + (Vector3)(Vector2.down * DataEntity.LedgeCheckDistance));
        Gizmos.DrawWireSphere(_playerCheck.position + (Vector3)(Vector2.right * DataEntity.CloseRangeActionDistance), 0.2f);
        Gizmos.DrawWireSphere(_playerCheck.position + (Vector3)(Vector2.right * DataEntity.MinAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(_playerCheck.position + (Vector3)(Vector2.right * DataEntity.MaxAgroDistance), 0.2f);
    }
}
