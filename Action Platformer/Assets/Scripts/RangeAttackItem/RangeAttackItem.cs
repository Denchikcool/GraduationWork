using System;
using UnityEngine;
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

    public event Action<GameObject> OnDamageHit;

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

    private void FixedUpdate()
    {
        if (!_hasHitGround)
        {
            Collider2D damageHit = Physics2D.OverlapCircle(_damagePosition.position, _damageRadius, _whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(_damagePosition.position, _damageRadius, _whatIsGround);

            if (damageHit)
            {
                Transform mainHeroTransform = damageHit.transform.GetComponent<Transform>();
                Core targetCore = mainHeroTransform.GetComponentInChildren<Core>();

                if (targetCore != null)
                {
                    Transform combatTransform = targetCore.transform.Find("Combat");
                    if (combatTransform != null)
                    {
                        OnDamageHit?.Invoke(combatTransform.gameObject);
                    }
                    else
                    {
                        Debug.LogWarning("Combat object not found in Core: " + targetCore.transform.name);
                        OnDamageHit?.Invoke(null);
                    }
                }
                else
                {
                    Debug.LogWarning("Core not found in parent hierarchy of " + damageHit.transform.name);
                    OnDamageHit?.Invoke(null);
                }
                Destroy(gameObject);
            }
            if (groundHit)
            {
                Debug.Log(groundHit.transform.name);
                Debug.Log("Ground hit!");
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