using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //private AttackDetails _attackDetails;

    private float _itemSpeed;
    private float _itemTravelDistance;
    private float _xStartPosition;
    
    private bool _isGravityOn;
    private bool _hasHitGround;

    private Rigidbody2D _itemRigidBody;

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
            //_attackDetails.Position = transform.position;

            if (_isGravityOn)
            {
                float angle = Mathf.Atan2(_itemRigidBody.velocity.y, _itemRigidBody.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!_hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(_damagePosition.position, _damageRadius, _whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(_damagePosition.position, _damageRadius, _whatIsGround);

            if (damageHit)
            {
                //damageHit.transform.SendMessage("TakeDamage", _attackDetails);
                Destroy(gameObject);
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

    public void FireRangeAttackItem(float speed, float travelDistance, float damage)
    {
        this._itemSpeed = speed;
        this._itemTravelDistance = travelDistance;
        //_attackDetails.DamageAmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_damagePosition.position, _damageRadius);
    }
}
