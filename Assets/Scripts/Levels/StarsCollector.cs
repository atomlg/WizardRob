using System;
using Coins;
using Player;
using UnityEngine;

namespace Levels
{
    public class StarsCollector : MonoBehaviour
    {
        [SerializeField] private TimerPanel _timerPanel;
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private CoinsCollector coinsCollector;
    
        private int _starsCount;
    
        public event Action<int> Collected;
        private void OnEnable()
        {
            _timerPanel.TimeIsOver += CollectStars;
        }

        private void OnDisable()
        {
            _timerPanel.TimeIsOver -= CollectStars;
        }
    
        private void CollectStars()
        {
            _starsCount++;

            if (_playerHealth.HealthIsFull)
                _starsCount++;
        
            if(coinsCollector.AllCoinsCollected)
                _starsCount++;
        
            Collected?.Invoke(_starsCount);
        }
    }
}
