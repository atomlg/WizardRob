using Player;
using UnityEngine;

namespace Coins
{
    public class CoinsCollector : MonoBehaviour
    {
        private int  _maxCoinsOnLevel;
        private int _collectedCount;

        private AudioSource _audioSource;

        public bool AllCoinsCollected => _collectedCount == _maxCoinsOnLevel;
        
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        
        private void Start()
        {
            _maxCoinsOnLevel = transform.childCount;
            SubscribeOnCoins();
        }

        private void SubscribeOnCoins()
        {
            foreach (Coin coin in GetComponentsInChildren<Coin>())
            {
                coin.Collected += OnCoinCollected;
            }
        }

        private void OnCoinCollected(Coin coin)
        {
            _audioSource.Play();
            _collectedCount++;
            coin.Collected -= OnCoinCollected;
        }
    }
}
