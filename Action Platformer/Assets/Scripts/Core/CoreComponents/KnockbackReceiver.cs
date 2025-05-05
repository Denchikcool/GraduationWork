using Denchik.Interfaces;
using UnityEngine;

namespace Denchik.CoreSystem
{
    public class KnockbackReceiver : CoreComponent, IKnockbackable
    {
        [SerializeField]
        private float _maxKnockbackTime = 0.2f;

        private bool _isKnockbackActive;

        private float _knockbackStartTime;

        private CoreComp<Movement> _movement;
        private CoreComp<CollisionSenses> _collisionSenses;

        protected override void Awake()
        {
            base.Awake();

            _movement = new CoreComp<Movement>(core);
            _collisionSenses = new CoreComp<CollisionSenses>(core);
        }

        public override void UpdateLogic()
        {
            CheckKnockback();
        }

        public void Knockback(Vector2 angle, float strength, int direction)
        {
            _movement.Component?.SetVelocity(strength, angle, direction);
            _movement.Component.CanSetVelocity = false;
            _isKnockbackActive = true;
            _knockbackStartTime = Time.time;
        }

        private void CheckKnockback()
        {
            if (_isKnockbackActive && ((_movement.Component?.CurrentVelocity.y <= 0.01f && _collisionSenses.Component.TouchingGround) || Time.time >= _knockbackStartTime + _maxKnockbackTime))
            {
                _isKnockbackActive = false;
                _movement.Component.CanSetVelocity = true;
            }
        }
    }
}
