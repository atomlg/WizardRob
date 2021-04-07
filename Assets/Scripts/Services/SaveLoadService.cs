using Data;
using Extensions;
using UnityEngine;

namespace Services
{
    public class SaveLoadService : MonoBehaviour
    {
        private const string Key = "GameData";
    
        public GameData LoadGameData()
        {
            return PlayerPrefs.GetString(Key)?
                .FromJson<GameData>();
        }
        
        public void SaveGameData(GameData gameData)
        {
            PlayerPrefs.SetString(Key, gameData.ToJson());
        }
    }
}
