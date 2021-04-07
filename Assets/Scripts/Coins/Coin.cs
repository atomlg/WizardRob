using System;
using Player;
using UnityEngine;

namespace Coins
{
    public class Coin : MonoBehaviour
    {
        private bool _isCollided;

        public event Action<Coin> Collected;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_isCollided)
                return;
            
            if (other.TryGetComponent(out PlayerBodyPart bodyPart))
            {
                _isCollided = true;
                Collected?.Invoke(this);
                Destroy(gameObject);
            }
        }
    }
}
