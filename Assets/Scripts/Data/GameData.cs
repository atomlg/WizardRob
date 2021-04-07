using System;

namespace Data
{
    [Serializable]
    public class GameData
    {
        public LevelsProgressData LevelsProgressData;
        public GameSettingsData GameSettingsData;
    
        public GameData()
        {
            LevelsProgressData = new LevelsProgressData();
            GameSettingsData = new GameSettingsData();
        }
    }
}
