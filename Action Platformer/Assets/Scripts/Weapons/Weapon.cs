using System;
using UnityEngine;
using Denchik.Utilities;
using Denchik.CoreSystem;

namespace Denchik.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [field: SerializeField]
        public WeaponData WeaponData { get; private set; }
        [SerializeField]
        private float _attackCounterResetCooldown;

        private Animator _animator;
        
        private Timer _attackCounterResetTimer;

        private int _currentAttackCounter;

        public event Action OnExit;
        public event Action OnEnter;

        public AnimationEventHandler EventHandler { get; private set; }

        public Core Core { get; private set; }

        public int CurrentAttackCounter
        {
            get => _currentAttackCounter;
            private set
            {
                _currentAttackCounter = value >= WeaponData.NumberOfAttacks ? 0 : value;
            }
        }
        public GameObject MainHeroGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }


        private void Awake()
        {
            MainHeroGameObject = transform.Find("MainHeroMotion").gameObject;
            WeaponSpriteGameObject = transform.Find("WeaponSprite").gameObject;

            _animator = MainHeroGameObject.GetComponent<Animator>();
            EventHandler = MainHeroGameObject.GetComponent<AnimationEventHandler>();

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
            EventHandler.OnFinished += Exit;
            _attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
        }

        private void OnDisable()
        {
            EventHandler.OnFinished -= Exit;
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

        public void SetCore(Core core)
        {
            Core = core;
        }
    }
}

