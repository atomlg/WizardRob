using System;
using Player;
using Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Levels
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class LevelFinisher : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _effects;
        [SerializeField] private GameDataService _gameDataService;
       
        [SerializeField] protected StarsCollector StarsCollector;
        [SerializeField] protected PlayerHealth PlayerHealth;
        
        private int _currentLevelNumber;

        private AudioSource _audioSource;

        public event Action<int> NextLevelIsReady;
        public event Action LevelFinished;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _currentLevelNumber = int.Parse(SceneManager.GetActiveScene().name);
        }

        protected void WonGame(int starsCount)
        {
            UpdateProgress(starsCount);
            TryToGetNextLevel();
            PlayEffects();
            PlayAudio();
            LevelFinished?.Invoke();
        }

        private void PlayAudio()
        {
            _audioSource.Play();
        }

        private void PlayEffects()
        {
            foreach (ParticleSystem effect in _effects)
            {
                effect.Play();
            }
        }

        protected void LoseGame()
        {
            TryToGetNextLevel();
            LevelFinished?.Invoke();
        }

        private void TryToGetNextLevel()
        {
            int nextLevelNumber = _currentLevelNumber + 1;
            if(_gameDataService.LevelsProgress.NextLevelIsReady(nextLevelNumber))
            {
                NextLevelIsReady?.Invoke(nextLevelNumber);
            }
        }
        
        private void UpdateProgress(int starsCount)
        {
            _gameDataService.LevelsProgress.UpdateProgress(_currentLevelNumber, starsCount);
        }
    }
}