using Denchik.Utilities;
using UnityEngine;

namespace Denchik.ProjectileSystem.Components
{
    public class DelayGravity : ProjectileComponent
    {
        [field: SerializeField]
        public float Gravity { get; private set; }

        [field: SerializeField]
        public float Distance { get; private set; }

        private DistanceNotifier _distanceNotifier = new DistanceNotifier();

        protected override void Awake()
        {
            base.Awake();

            _distanceNotifier.OnNotify += HandleNotify;
        }

        protected override void Start()
        {
            base.Start();

            Rigidbody2D.gravityScale = 0.0f;
        }

        protected override void Update()
        {
            base.Update();

            _distanceNotifier?.Tick(transform.position);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _distanceNotifier.OnNotify -= HandleNotify;
        }

        private void HandleNotify()
        {
            Rigidbody2D.gravityScale = Gravity;
        }

        protected override void Init()
        {
            base.Init();

            Rigidbody2D.gravityScale = 0.0f;
            _distanceNotifier.Init(transform.position, Distance);
        }
    }
}
