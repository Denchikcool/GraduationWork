using System;
using UnityEngine;

namespace Denchik.Weapon
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinished;
        public event Action OnStartMovement;
        public event Action OnStopMovement;

        private void AnimationFinishedTrigger()
        {
            OnFinished?.Invoke();
        }

        private void StartMovementTrigger()
        {
            OnStartMovement?.Invoke();
        }

        private void StopMovementTrigger()
        {
            OnStopMovement?.Invoke();
        }
    }
}
