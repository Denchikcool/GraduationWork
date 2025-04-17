using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    private Transform _wallCheck;
    [SerializeField]
    private Transform _ledgeCheck;

    public FinalStateMachine FinalStateMachine;

    public DataEntity DataEntity;

    private Vector2 _velocityWorkSpace;

    public int FacingDirection { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    public GameObject AliveGameObject { get; private set; }

    public virtual void Start()
    {
        FacingDirection = 1;

        AliveGameObject = transform.Find("Alive").gameObject;
        Rigidbody = AliveGameObject.GetComponent<Rigidbody2D>();
        Animator = AliveGameObject.GetComponent<Animator>();

        FinalStateMachine = new FinalStateMachine();
    }

    public virtual void Update()
    {
        FinalStateMachine.CurrentState.UpdateLogic();
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

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(_wallCheck.position, AliveGameObject.transform.right, DataEntity.WallCheckDistance, DataEntity.WhatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(_ledgeCheck.position, Vector2.down, DataEntity.LedgeCheckDistance, DataEntity.WhatIsGround);
    }

    public virtual void Flip()
    {
        FacingDirection *= -1;
        AliveGameObject.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(_wallCheck.position, _wallCheck.position + (Vector3)(Vector2.right * FacingDirection * DataEntity.WallCheckDistance));
        Gizmos.DrawLine(_ledgeCheck.position, _ledgeCheck.position + (Vector3)(Vector2.down * DataEntity.LedgeCheckDistance));
    }
}
