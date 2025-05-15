using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Denchik.ProjectileSystem.Components
{
    public class RotateTowardsVelocity : ProjectileComponent
    {
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            Vector2 velocity = Rigidbody2D.velocity;

            if (velocity.Equals(Vector3.zero))
            {
                return;
            }

            float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
