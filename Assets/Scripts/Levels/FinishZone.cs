using System;
using UnityEngine;

namespace Levels
{
    public class FinishZone : MonoBehaviour
    {
        private bool _triggerEntered;
        private bool _triggerExited;

        public event Action TriggerEnter;
        public event Action TriggerExit;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_triggerEntered)
                return;

            _triggerExited = false;
            _triggerEntered = true;

            TriggerEnter?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_triggerExited)
                return;

            _triggerExited = true;
            _triggerEntered = false;
            TriggerExit?.Invoke();
        }
    }
}