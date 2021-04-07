using System;
using Levels;
using Traps;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(HingeJoint2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    
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