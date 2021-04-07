using Services;
using TMPro;
using UnityEngine;

namespace Menu
{
    public class StarsCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _totalStars;
        [SerializeField] private GameDataService _gameDataService;

        private void OnEnable()
        {
            _gameDataService.StarsUpdated += OnStarsUpdated;
        }
        
        private void OnDisable()
        {
            _gameDataService.StarsUpdated -= OnStarsUpdated;
        }

        private void OnStarsUpdated(int totalEarnedStars, int starsCanBeEarned)
        {
            _totalStars.text = $"{totalEarnedStars}/{starsCanBeEarned}";
        }
    }
}
