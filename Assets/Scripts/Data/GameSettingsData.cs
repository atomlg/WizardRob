using System;

namespace Data
{
    [Serializable]
    public class GameSettingsData
    {
        public GameVolumeSettings GameVolume;
        
        public event Action SettingsUpdated;

        public void UpdateSettings(GameVolumeSettings gameVolumeSettings)
        {
            GameVolume = gameVolumeSettings;
            SettingsUpdated?.Invoke();
        }
    }
}
