using System;
using Data;
using UnityEngine;

namespace Services
{
    public class GameDataService : MonoBehaviour
    {
        [SerializeField] private SaveLoadService _saveLoadService;
        
        private GameData _gameData;
        private LevelsProgressData _levelsProgress;
        private GameSettingsData _gameSettings;

        public LevelsProgressData LevelsProgress => _levelsProgress;
        public GameSettingsData GameSettings => _gameSettings;

        public event Action DataLoaded;
        public event Action DataCreated;
        public event Action<int, int> StarsUpdated;
        private void Start()
        {
            LoadOrCreateGameData();
        }
    
        private void OnDisable()
        {
            _levelsProgress.ProgressUpdated -= SaveGameData;
            _gameSettings.SettingsUpdated -= SaveGameData;
        }

        private void LoadOrCreateGameData()
        {
            _gameData = _saveLoadService.LoadGameData();
            if (_gameData == null)
            {
                _gameData = new GameData();
                SetData(_gameData.LevelsProgressData, _gameData.GameSettingsData);
                DataCreated?.Invoke();
            }
            else
            {
                SetData(_gameData.LevelsProgressData, _gameData.GameSettingsData);
                DataLoaded?.Invoke();
            }

            _levelsProgress.ProgressUpdated += SaveGameData;
            _gameSettings.SettingsUpdated += SaveGameData;
        
            SaveGameData();
            StarsUpdated?.Invoke(_levelsProgress.StarsEarned, _levelsProgress.StarsCanBeEarned);
        }

        private void SetData(LevelsProgressData levelsProgressData, GameSettingsData gameSettingsData)
        {
            _levelsProgress = levelsProgressData;
            _gameSettings = gameSettingsData;
        }

        private void SaveGameData()
        {
            _saveLoadService.SaveGameData(_gameData);
        }
    }
}
