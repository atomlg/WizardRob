using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Levels
{
    public abstract class LevelMenu : MonoBehaviour
    {
        private const string MenuSceneName = "Menu";

        [Header("Pause Panel")]
        [SerializeField] private Button _restartLevelButtonPause;
        [SerializeField] private Button _loadMenuButtonPause;
        [SerializeField] private Button _openPausePanelButton;
        [SerializeField] private Button _closePausePanelButton;
        [SerializeField] private Button _openSettingsPanelButton;
        [SerializeField] private GameObject _pausePanel;
    
        [Header("Game Settings Panel")]
        [SerializeField] private GameObject _gameSettingsPanel;
        [SerializeField] private Button _closeSettingsPanelButton;
    
        [Header("Finish Level Panel")]
        [SerializeField] private Button _restartLevelButtonFinish;
        [SerializeField] private Button _loadMenuButtonFinish;
        [SerializeField] private Button _nextLevelButton;
        [SerializeField] private GameObject _finishLevelPanel;

        [SerializeField] private LevelFinisher _levelFinisher;

        [SerializeField] private Image[] _starsImages;
        [SerializeField] private Sprite _fullStarSprite;

        [SerializeField] protected StarsCollector StarsCollector;
        
        private int _nextLevelNumber;
        private bool _isPaused;
    
        protected virtual void OnEnable()
        {
            _levelFinisher.NextLevelIsReady += OnNextLevelIsReady;
            _levelFinisher.LevelFinished += OnLevelFinished;
        
            _restartLevelButtonPause.onClick.AddListener(RestartLevel);
            _loadMenuButtonPause.onClick.AddListener(LoadMenu);
            _openPausePanelButton.onClick.AddListener(OpenPausePanel);
            _closePausePanelButton.onClick.AddListener(ClosePausePanel);
            _openSettingsPanelButton.onClick.AddListener(OpenSettingPanel);

            _closeSettingsPanelButton.onClick.AddListener(CloseSettingPanel);

            _nextLevelButton.onClick.AddListener(LoadNextLevel);
            _restartLevelButtonFinish.onClick.AddListener(RestartLevel);
            _loadMenuButtonFinish.onClick.AddListener(LoadMenu);
        }

        protected virtual void OnDisable()
        {
            _levelFinisher.NextLevelIsReady -= OnNextLevelIsReady;
            _levelFinisher.LevelFinished -= OnLevelFinished;
        
            _restartLevelButtonPause.onClick.RemoveListener(RestartLevel);
            _loadMenuButtonPause.onClick.RemoveListener(LoadMenu);
            _openPausePanelButton.onClick.RemoveListener(OpenPausePanel);
            _closePausePanelButton.onClick.RemoveListener(ClosePausePanel);
            _openSettingsPanelButton.onClick.RemoveListener(OpenSettingPanel);

            _closeSettingsPanelButton.onClick.RemoveListener(CloseSettingPanel);

            _nextLevelButton.onClick.RemoveListener(LoadNextLevel);
            _restartLevelButtonFinish.onClick.RemoveListener(RestartLevel);
            _loadMenuButtonFinish.onClick.RemoveListener(LoadMenu);
        }

        private void OpenSettingPanel()
        {
            _pausePanel.SetActive(false);
            _gameSettingsPanel.SetActive(true);
        }
    
        private void CloseSettingPanel()
        {
            _isPaused = false;
            _gameSettingsPanel.SetActive(false);
            StartTime();
            _openPausePanelButton.gameObject.SetActive(true);
        }

        protected void OnStarsCollected(int starsCount)
        {
            for (int i = 0; i < starsCount; i++)
            {
                _starsImages[i].sprite = _fullStarSprite;
            }
        }

        private void OpenPausePanel()
        {
            _openPausePanelButton.gameObject.SetActive(false);
            _pausePanel.SetActive(true);
            _isPaused = true;
            StopTime();
        }

        private void ClosePausePanel()
        {
            _openPausePanelButton.gameObject.SetActive(true);
            StartTime();
            _isPaused = false;
            _pausePanel.SetActive(false);
        }

        private void LoadNextLevel()
        {
            SceneManager.LoadScene(_nextLevelNumber.ToString());
        }

        private void RestartLevel()
        {
            if(_isPaused)
                StartTime();
        
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        private void LoadMenu()
        {
            if(_isPaused)
                StartTime();
        
            SceneManager.LoadScene(MenuSceneName);
        }

        private void OnNextLevelIsReady(int nextLevelNumber)
        {
            _nextLevelButton.interactable = true;
            _nextLevelNumber = nextLevelNumber;
        }

        private void OnLevelFinished()
        {
            _openPausePanelButton.gameObject.SetActive(false);
            _finishLevelPanel.SetActive(true);
        }

        private void StopTime()
        {
            Time.timeScale = 0;
        }

        private void StartTime()
        {
            Time.timeScale = 1;
        }
    }
}
