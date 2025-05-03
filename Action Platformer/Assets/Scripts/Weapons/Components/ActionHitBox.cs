using Denchik.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Denchik.Weapon.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        private CoreComp<CoreSystem.Movement> _movement;

        private Vector2 _offSet;

        private Collider2D[] _detected;

        private event Action<Collider2D[]> OnDetectedCollider;

        protected override void Start()
        {
            base.Start();

            _movement = new CoreComp<CoreSystem.Movement>(core);
        }

        private void HandleAttackAction()
        {
            _offSet.Set(transform.position.x + (currentAttackData.HitBox.center.x * _movement.Component.FacingDirection), transform.position.y + currentAttackData.HitBox.center.y);
            _detected = Physics2D.OverlapBoxAll(_offSet, currentAttackData.HitBox.size, 0.0f, data.DetectableLayers);

            if(_detected.Length == 0)
            {
                return;
            }

            OnDetectedCollider?.Invoke(_detected);

            foreach(Collider2D collider in _detected)
            {
                Debug.Log(collider.name);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            eventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void OnDisable()
        {
            eventHandler.OnAttackAction -= HandleAttackAction;
        }

        private void OnDrawGizmosSelected()
        {
            if(data == null)
            {
                return;
            }

            foreach(AttackActionHitBox box in data.AttackData)
            {
                if (!box.Debug)
                {
                    continue;
                }
                Gizmos.DrawWireCube(transform.position + (Vector3)box.HitBox.center, box.HitBox.size);
            }
        }
    }
}
