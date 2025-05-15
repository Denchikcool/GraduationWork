using Denchik.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Denchik.ProjectileSystem.Components
{
    [RequireComponent(typeof(HitBox))]
    public class StickToLayer : ProjectileComponent
    {
        [field: SerializeField]
        public LayerMask LayerMask { get; private set; }

        [field: SerializeField]
        public float CheckDistance { get; private set; }

        [field: SerializeField]
        public string InactiveSortingLayerName { get; private set; }


        private bool _isStuck;
        private bool _subscribedToDisableNotifier;

        private string _activeSortingLayerName;

        private HitBox _hitBox;

        private SpriteRenderer _spriteRenderer;

        private OnDisableNotifier _onDisableNotifier;

        private Transform _referenceTransform;
        private Transform _transform;

        private Vector3 _offsetPosition;

        private Quaternion _offsetRotation;


        protected override void Awake()
        {
            base.Awake();

            _transform = transform;

            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _activeSortingLayerName = _spriteRenderer.sortingLayerName;

            _hitBox = GetComponent<HitBox>();

            _hitBox.OnRayCastHit2D += HandleRaycastHit2D;
        }

        protected override void Update()
        {
            base.Update();

            if (!_isStuck)
            {
                return;
            }

            Quaternion referenceRotation = _referenceTransform.rotation;
            _transform.position = _referenceTransform.position + referenceRotation * _offsetPosition;
            _transform.rotation = referenceRotation * _offsetRotation;
        }

        protected override void Init()
        {
            base.Init();

            _isStuck = false;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _hitBox.OnRayCastHit2D -= HandleRaycastHit2D;

            if (_subscribedToDisableNotifier)
            {
                _onDisableNotifier.OnDisableEvent -= HandleDisableNotifier;
            }
        }

        private void HandleRaycastHit2D(RaycastHit2D[] hits)
        {
            if (_isStuck)
            {
                return;
            }

            SetStuck();

            RaycastHit2D lineHit = Physics2D.Raycast(_transform.position, _transform.right, CheckDistance, LayerMask);

            if (lineHit)
            {
                SetReferenceTransformAndPoint(lineHit.transform, lineHit.point);
                return;
            }

            foreach(RaycastHit2D hit in hits)
            {
                if(!LayerMaskUtilities.IsLayerInMask(hit, LayerMask))
                {
                    continue;
                }

                SetReferenceTransformAndPoint(lineHit.transform, lineHit.point);
                return;
            }

            SetUnstuck();
        }

        private void SetReferenceTransformAndPoint(Transform newReferenceTransform, Vector2 newPoint)
        {
            if(newReferenceTransform.TryGetComponent(out _onDisableNotifier))
            {
                _onDisableNotifier.OnDisableEvent += HandleDisableNotifier;
                _subscribedToDisableNotifier = true;
            }

            _transform.position = newPoint;

            _referenceTransform = newReferenceTransform;
            _offsetPosition = _transform.position - _referenceTransform.position;
            _offsetRotation = Quaternion.Inverse(_referenceTransform.rotation) * _transform.rotation;
        }

        private void SetStuck()
        {
            _isStuck = true;

            _spriteRenderer.sortingLayerName = InactiveSortingLayerName;
            Rigidbody2D.velocity = Vector2.zero;
            Rigidbody2D.bodyType = RigidbodyType2D.Static;
        }

        private void SetUnstuck()
        {
            _isStuck = false;

            _spriteRenderer.sortingLayerName = _activeSortingLayerName;
            Rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        }

        private void HandleDisableNotifier()
        {
            SetUnstuck();

            if (!_subscribedToDisableNotifier)
            {
                return;
            }

            _onDisableNotifier.OnDisableEvent -= HandleDisableNotifier;
            _subscribedToDisableNotifier = false;
        }
    }
}
