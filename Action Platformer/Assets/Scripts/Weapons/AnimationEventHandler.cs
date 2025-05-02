using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Denchik.Weapon
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinished;
        private void AnimationFinishedTrigger()
        {
            OnFinished?.Invoke();
        }
    }
}
