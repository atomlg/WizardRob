using UnityEngine;

namespace Traps
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BossTrapHealth : TrapHealth
    {
        [SerializeField] private Sprite[] _statesSprite;
        [SerializeField] private GameObject _finishZone;
    
        private SpriteRenderer _spriteRenderer;
    
        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void TakeDamage()
        {
            if (CurrentHealth > 0)
            {
                CurrentHealth--;
            
                if (CurrentHealth <= 0)
                {
                    Die();
                }
                else
                {
                    _spriteRenderer.sprite = _statesSprite[CurrentHealth - 1];
                }
            }

            IsCollided = false;
        }

        protected override void Die()
        {
            _finishZone.SetActive(true);
            base.Die();
        }
    }
}
