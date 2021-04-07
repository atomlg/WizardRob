using UnityEngine;

namespace Levels
{
    public class NormalLevelFinisher : LevelFinisher
    {
        private void OnEnable()
        {
            StarsCollector.Collected += WonGame;
            PlayerHealth.Died += LoseGame;
        }
        
        private void OnDisable()
        {
            StarsCollector.Collected -= WonGame;
            PlayerHealth.Died -= LoseGame;
        }
    }
}
