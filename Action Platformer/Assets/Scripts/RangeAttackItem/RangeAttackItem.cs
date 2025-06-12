using Denchik.Interfaces;
using UnityEngine;
using Denchik.ProjectileSystem.Components;
using Denchik.CoreSystem.StatsSystem;
using Denchik.CoreSystem;

public class RangeAttackItem : MonoBehaviour
{
    [SerializeField]
    private float _gravity;
    [SerializeField]
    private float _damageRadius;

    [SerializeField]
    private LayerMask _whatIsGround;
    [SerializeField]
    private LayerMask _whatIsPlayer;

    [SerializeField]
    private Transform _damagePosition;


    private float _itemSpeed;
    private float _itemTravelDistance;
    private float _xStartPosition;

    private bool _isGravityOn;
    private bool _hasHitGround;

    private Rigidbody2D _itemRigidBody;

    private DataRangeAttackState _dataRangeAttackState;

    private Core _core;
    private Stats _stats;

    public Stats Stats
    {
        get => _stats ? _stats : _core.GetCoreComponent(ref _stats);
    }

    private void Start()
    {
        _itemRigidBody = GetComponent<Rigidbody2D>();

        _itemRigidBody.gravityScale = 0.0f;
        _itemRigidBody.velocity = transform.right * _itemSpeed;

        _xStartPosition = transform.position.x;

        _isGravityOn = false;
    }

    private void Update()
    {
        if (!_hasHitGround)
        {

            if (_isGravityOn)
            {
                float angle = Mathf.Atan2(_itemRigidBody.velocity.y, _itemRigidBody.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private Collider2D FixedUpdate()
    {
        if (!_hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(_damagePosition.position, _damageRadius, _whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(_damagePosition.position, _damageRadius, _whatIsGround);

            if (damageHit)
            {
               Destroy(gameObject);
               return damageHit;
            }
            if (groundHit)
            {
                _hasHitGround = true;
                _itemRigidBody.gravityScale = 0.0f;
                _itemRigidBody.velocity = Vector2.zero;
            }

            if (Mathf.Abs(_xStartPosition - transform.position.x) >= _itemTravelDistance && !_isGravityOn)
            {
                _isGravityOn = true;
                _itemRigidBody.gravityScale = _gravity;
            }
        }
    }

    public void FireRangeAttackItem(float speed, float travelDistance)
    {
        this._itemSpeed = speed;
        this._itemTravelDistance = travelDistance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_damagePosition.position, _damageRadius);
    }
}