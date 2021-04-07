using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerHealth : MonoBehaviour
    {
        private const int MaxHealth = 6;

        [SerializeField] private AudioClip _dyingAudio;

        private int _currentHealth;
        private AudioSource _audioSource;

        public bool HealthIsFull => _currentHealth == MaxHealth;
        public event Action Damaged;
        public event Action Died;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _currentHealth = MaxHealth;
        }

        public void TakeDamage()
        {
            if (_currentHealth > 0)
            {
                _currentHealth--;
                PlayAudio();
                Damaged?.Invoke();

                if (_currentHealth <= 0)
                    Die();
            }
        }

        private void PlayAudio()
        {
            _audioSource.Play();
        }

        private void SetAudiClip(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
        }

        private void Die()
        {
            SetAudiClip(_dyingAudio);
            PlayAudio();
            Died?.Invoke();
        }
    }
}