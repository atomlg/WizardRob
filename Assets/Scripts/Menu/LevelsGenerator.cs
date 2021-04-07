using System.Collections.Generic;
using Data;
using Services;
using UnityEngine;

namespace Menu
{
    public class LevelsGenerator : MonoBehaviour
    {
        [SerializeField] private LevelUI _level;
        [SerializeField] private Transform _content;
        [SerializeField] private List<LevelData> _levelsData;
        [SerializeField] private GameDataService _gameDataService;

        private void OnEnable()
        {
            _gameDataService.DataCreated += OnDataCreated;
            _gameDataService.DataLoaded += OnDataLoaded;
        }
    
        private void OnDisable()
        {
            _gameDataService.DataCreated -= OnDataCreated;
            _gameDataService.DataLoaded -= OnDataLoaded;
        }

        private void OnDataCreated()
        {
            UpdateLevels();
            CreateLevels();
        }

        private void OnDataLoaded()
        {
            CompareLevels();
            CreateLevels();
        }

        private void CreateLevels()
        {
            for (int i = 0; i < _levelsData.Count; i++)
            {
                LevelUI level = Instantiate(_level, _content);
                level.UpdateLevel(_gameDataService.LevelsProgress.TryGetLevel(i), _gameDataService.LevelsProgress.StarsEarned);
                level.name = $"Level{i+1}";
            }
        }

        private void CompareLevels()
        {
            if (_levelsData.Count > _gameDataService.LevelsProgress.LevelsData.Count)
            {
                for (int i = _gameDataService.LevelsProgress.LevelsData.Count; i < _levelsData.Count; i++)
                {
                    _gameDataService.LevelsProgress.AddLevel(_levelsData[i]);
                }
            }
            else if (_levelsData.Count < _gameDataService.LevelsProgress.LevelsData.Count)
            {
                for (int i = _levelsData.Count; i < _gameDataService.LevelsProgress.LevelsData.Count; i++)
                {
                    _gameDataService.LevelsProgress.RemoveLevel(i);
                }
            }
        
            UpdateLevels();
        }

        private void UpdateLevels()
        {
            _gameDataService.LevelsProgress.UpdateLevels(_levelsData);
        }
    }
}
