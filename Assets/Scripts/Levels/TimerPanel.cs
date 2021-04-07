using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Levels
{
    public class TimerPanel : MonoBehaviour
    {
        private const float WaitingTime = 3f;
        
        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private Image _cloudImage;
        [SerializeField] private FinishZone _finishZone;
        
        private IEnumerator _timerRoutine;

        public event Action TimeIsOver;
        
        private void OnEnable()
        {
            _finishZone.TriggerEnter += OnPlayerTriggerEnter;
            _finishZone.TriggerExit += OnPlayerTriggerExit;
        }

        private void OnDisable()
        {
            _finishZone.TriggerEnter -= OnPlayerTriggerEnter;
            _finishZone.TriggerExit -= OnPlayerTriggerExit;
        }

        private void Start()
        {
            DisableElements();
        }

        private void OnPlayerTriggerEnter()
        {
            _timerRoutine = StartTimer();
            StartCoroutine(_timerRoutine);
        }

        private void OnPlayerTriggerExit()
        {
            StopTimer();
        }

        public void Enable()
        {
            _textMesh.enabled = true;
            _cloudImage.enabled = true;
        }

        private void ChangeTime(float time)
        {
            _textMesh.text = time.ToString("0.0");
        }
        
        private IEnumerator StartTimer()
        {
            EnableElements();
        
            float time = WaitingTime;
            while (time > 0)
            {
                ChangeTime(time);
                time -= Time.unscaledDeltaTime;
                yield return null;
            }
        
            StopTimer();
            _timerRoutine = null;
            TimeIsOver?.Invoke();
        }

        private void StopTimer()
        {
            if (_timerRoutine != null)
            {
                StopCoroutine(_timerRoutine);
                _timerRoutine = null;
            }

            DisableElements();
        }

        private void EnableElements()
        {
            _textMesh.enabled = true;
            _cloudImage.enabled = true;
        }

        private void DisableElements()
        {
            _textMesh.enabled = false;
            _cloudImage.enabled = false;
        }
    }
}
