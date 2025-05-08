using System;
using UnityEngine;

namespace Denchik.Utilities
{
    public class DistanceNotifier
    {
        private Vector3 _referencePos;

        private float _distance;
        private float _sqrDistance;

        private bool _checkInside;
        private bool _enabled;

        public event Action OnNotify;

        public void Init(Vector3 referencePos, float distance, bool checkInside = false, bool triggerContinuously = false)
        {
            this._referencePos = referencePos;
            this._distance = distance;

            _sqrDistance = distance * distance;

            this._checkInside = checkInside;

            _enabled = true;

            if(!triggerContinuously)
            {
                OnNotify += Disable;
            }
        }

        public void Disable()
        {
            _enabled = false;
            OnNotify -= Disable;
        }

        public void Tick(Vector3 pos)
        {
            if (!_enabled)
            {
                return;
            }

            var currentSqrDistance = (_referencePos - pos).sqrMagnitude;

            if (_checkInside)
            {
                CheckInside(currentSqrDistance);
            }
            else
            {
                CheckOutside(currentSqrDistance);
            }
        }

        private void CheckInside(float distance)
        {
            if(distance <= _sqrDistance)
            {
                OnNotify?.Invoke();
            }
        }

        private void CheckOutside(float distance)
        {
            if(distance >= _sqrDistance)
            {
                OnNotify?.Invoke();
            }
        }
    }
}
