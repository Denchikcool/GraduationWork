using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Denchik.Utilities;
using Denchik.Weapon.Components;

namespace Denchik.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private int _numberOfAttacks;
        [SerializeField]
        private float _attackCounterResetCooldown;

        private Animator _animator;
        
        private AnimationEventHandler _animationEventHandler;
        private Timer _attackCounterResetTimer;

        private int _currentAttackCounter;

        public event Action OnExit;
        public event Action OnEnter;

        public int CurrentAttackCounter
        {
            get => _currentAttackCounter;
            private set
            {
                _currentAttackCounter = value >= _numberOfAttacks ? 0 : value;
            }
        }
        public GameObject MainHeroGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }


        private void Awake()
        {
            MainHeroGameObject = transform.Find("MainHeroMotion").gameObject;
            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;

            _animator = MainHeroGameObject.GetComponent<Animator>();
            _animationEventHandler = MainHeroGameObject.GetComponent<AnimationEventHandler>();

            _attackCounterResetTimer = new Timer(_attackCounterResetCooldown);
        }

        private void Update()
        {
            _attackCounterResetTimer.Tick();
        }

        private void Exit()
        {
            _animator.SetBool("active", false);
            CurrentAttackCounter++;
            _attackCounterResetTimer.StartTimer();
            OnExit?.Invoke();
        }

        private void ResetAttackCounter()
        {
            CurrentAttackCounter = 0;
        }

        private void OnEnable()
        {
            _animationEventHandler.OnFinished += Exit;
            _attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        }

        private void OnDisable()
        {
            _animationEventHandler.OnFinished -= Exit;
            _attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;
        }

        public void Enter()
        {
            print($"{transform.name} enter");

            _attackCounterResetTimer.StopTimer();

            _animator.SetBool("active", true);
            _animator.SetInteger("counter", CurrentAttackCounter);

            OnEnter?.Invoke();
        }
    }
}

