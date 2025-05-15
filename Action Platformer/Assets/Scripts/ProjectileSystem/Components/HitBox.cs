using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Denchik.ProjectileSystem.Components
{
    public class HitBox : ProjectileComponent
    {
        public event Action<RaycastHit2D[]> OnRayCastHit2D;

        [field: SerializeField]
        public Rect HitBoxRect { get; private set; }

        [field: SerializeField]
        public LayerMask LayerMask { get; private set; }

        private RaycastHit2D[] _hits;

        private Transform _transform;

        private float _checkDistance;


        protected override void Awake()
        {
            base.Awake();

            _transform = transform;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            _checkDistance = Rigidbody2D.velocity.magnitude * Time.deltaTime;

            CheckHitBox();
        }

        private void CheckHitBox()
        {
            _hits = Physics2D.BoxCastAll(transform.TransformPoint(HitBoxRect.center), HitBoxRect.size, _transform.rotation.eulerAngles.z, _transform.right, _checkDistance, LayerMask);

            if(_hits.Length <= 0)
            {
                return;
            }

            OnRayCastHit2D?.Invoke(_hits);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;

            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, Quaternion.Euler(0.0f, 0.0f, transform.rotation.eulerAngles.z), Vector3.one);

            Gizmos.matrix = rotationMatrix;

            Gizmos.DrawWireCube(HitBoxRect.center, HitBoxRect.size);
        }
    }
}
