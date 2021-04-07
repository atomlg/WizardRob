using Data;
using StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Menu
{
    public class LevelUI : MonoBehaviour
    {
        [SerializeField] private Image _cloud;
        [SerializeField] private Image _warningImage;
        [SerializeField] private TextMeshProUGUI _requiredStarsTMP;
        [SerializeField] private TextMeshProUGUI _numberTextMesh;
        [SerializeField] private Image[] _stars;
        [SerializeField] private GameObject _openedObject;
        [SerializeField] private GameObject _closedObject;
        [SerializeField] private Color _tutorialColor;
        [SerializeField] private Color _bossColor;

        private int _number;
        private int _starsRequired;
        private int _totalEarnedStars;
        private int _earnedStars;

        private Button _loadLevelButton;

        private void Awake()
        {
            _loadLevelButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _loadLevelButton.onClick.AddListener(Load);
        }

        private void OnDisable()
        {
            _loadLevelButton.onClick.RemoveListener(Load);
        }

        public void UpdateLevel(LevelData levelData, int totalEarnedStars)
        {
            _number = levelData.Number;
            _earnedStars = levelData.StarsEarned;
            _starsRequired = levelData.StarsRequired;
            _totalEarnedStars = totalEarnedStars;

            if (levelData.LevelId == LevelId.TutorialLevel)
            {
                _warningImage.enabled = true;
                _cloud.color = _tutorialColor;
            }
            else if (levelData.LevelId == LevelId.BossLevel)
            {
                _warningImage.enabled = true;
                _cloud.color = _bossColor;
            }
            
            if (CanOpen())
                Open();
            else
                Close();
        }

        private void Open()
        {
            _openedObject.SetActive(true);
            _numberTextMesh.text = _number.ToString();
            _loadLevelButton.interactable = true;

            for (int i = 0; i < _stars.Length; i++) 
                _stars[i].enabled = _earnedStars >= i + 1;
        }

        private void Close()
        {
            _closedObject.SetActive(true);
            _loadLevelButton.interactable = false;
            _requiredStarsTMP.text = _starsRequired.ToString();
        }

        private void Load()
        {
            SceneManager.LoadScene(_number.ToString());
        }

        private bool CanOpen()
        {
            return _totalEarnedStars >= _starsRequired;
        }
    }
}