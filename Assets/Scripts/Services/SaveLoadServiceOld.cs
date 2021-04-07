using Data;
using Extensions;
using UnityEngine;

namespace Services
{
    public static class SaveLoadServiceOld
    {
        private const string Key = "GameData";
        private const string ProgressKey = "Progress";
        private const string SettingsKey = "Settings";

        public static void SaveLevelsProgress(LevelsProgressData levelsProgressData)
        { 
            PlayerPrefs.SetString(ProgressKey, levelsProgressData.ToJson());
        }

        public static LevelsProgressData LoadLevelsProgress()
        {
            return PlayerPrefs.GetString(ProgressKey)?
                .FromJson<LevelsProgressData>();
        }
        
        public static void SaveGameSettingsData(GameSettingsData gameSettingsData)
        { 
            PlayerPrefs.SetString(SettingsKey, gameSettingsData.ToJson());
        }
        
        public static GameSettingsData LoadGameSettingsData()
        {
            return PlayerPrefs.GetString(SettingsKey)?
                .FromJson<GameSettingsData>();
        }

        public static GameData LoadGameData()
        {
            return PlayerPrefs.GetString(Key)?
                .FromJson<GameData>();
        }
        
        public static void SaveGameData(GameData gameData)
        { 
            PlayerPrefs.SetString(Key, gameData.ToJson());
        }
    }
}