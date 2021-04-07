using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Levels
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _playerHealth;
        [SerializeField] private  Image[] _heartsImages = new Image[5];
        
        private int _index;

        private void Start()
        {
            _index = _heartsImages.Length - 1;
        }

        private void OnEnable()
        {
            _playerHealth.Damaged += OnDamaged;
        }

        private void OnDisable()
        {
            _playerHealth.Damaged -= OnDamaged;
        }

        private void OnDamaged()
        {
            _heartsImages[_index].enabled = false;
            _index--;
        }
    }
}
