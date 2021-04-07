using System;
using Levels;
using Traps;
using UnityEngine;

namespace Player
{
    public class PlayerBodyPart : MonoBehaviour
    {
        private PlayerHealth _playerHealth;
        private bool _isDestroyed;

        private void Awake()
        {
            _playerHealth = GetComponentInParent<PlayerHealth>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(_isDestroyed)
                return;

            if (collision.gameObject.TryGetComponent(out Trap trap))
            {
                _playerHealth.TakeDamage();
                _isDestroyed = true;
                Destroy(gameObject);
            }
        }
    }
}