using System;
using UnityEngine;

namespace Denchik.Utilities
{
    public class Timer
    {
        private float _startTime;
        private float _duration;
        private float _targetTime;

        private bool _isActive;

        public event Action OnTimerDone;

        public Timer(float duration)
        {
            this._duration = duration;
        } 

        public void StartTimer()
        {
            _startTime = Time.time;
            _targetTime = _startTime + _duration;
            _isActive = true;
        }

        public void StopTimer()
        {
            _isActive = false;
        }

        public void Tick()
        {
            if (!_isActive)
            {
                return;
            }

            if(Time.time >= _targetTime)
            {
                OnTimerDone?.Invoke();
                StopTimer();
            }
        }
    }
}
