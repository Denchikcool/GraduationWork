using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Denchik.Weapon.Components
{
    public class InputHold : WeaponComponent
    {
        private Animator _animator;

        private bool _input;
        private bool _minHoldPassed;

        protected override void Awake()
        {
            base.Awake();

            _animator = GetComponentInChildren<Animator>();

            weapon.OnCurrentInputChanged += HandleCurrentInputChanged;
            eventHandler.OnMinHoldPassed += HandleMinHoldPassed;
        }

        protected override void HandleEnter()
        {
            base.HandleEnter();

            _minHoldPassed = false;
        }

        private void SetAnimatorParameter()
        {
            if (_input)
            {
                _animator.SetBool("hold", _input);
                return;
            }

            if (_minHoldPassed)
            {
                _animator.SetBool("hold", false);
            }
        }

        private void HandleCurrentInputChanged(bool newInput)
        {
            _input = newInput;

            SetAnimatorParameter();
        }

        private void HandleMinHoldPassed()
        {
            _minHoldPassed = true;

            SetAnimatorParameter();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            weapon.OnCurrentInputChanged -= HandleCurrentInputChanged;
            eventHandler.OnMinHoldPassed -= HandleMinHoldPassed;
        }
    }
}
