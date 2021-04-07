using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class LevelsProgressData
    {
        public const int  StarsPerLevel = 3;
        
        public int StarsEarned;
        public int StarsCanBeEarned;
        
        public List<LevelData> LevelsData;
        public event Action ProgressUpdated;

        public void UpdateLevels(List<LevelData> levelsData)
        {
            if (LevelsData == null)
            {
                LevelsData = levelsData;
                for (int i = 0; i < levelsData.Count; i++)
                {
                    LevelsData[i].Number = i;
                }
            }
            else
            {
                for (int i = 0; i < levelsData.Count; i++)
                {
                    LevelsData[i].Number = i;
                    LevelsData[i].LevelId = levelsData[i].LevelId;
                    LevelsData[i].StarsRequired = levelsData[i].StarsRequired;
                }
            }
            
            CalculateStars();
        }

        public void RemoveLevel(int index)
        {
            LevelsData.RemoveAt(index);
        }

        public void AddLevel(LevelData levelData)
        {
            LevelsData.Add(levelData);
        }

        public void UpdateProgress(int levelNumber, int earnedStars)
        {
            LevelData data = TryGetLevel(levelNumber);
            if (data != null)
            {
                if (earnedStars > data.StarsEarned)
                {
                    data.StarsEarned = earnedStars;
                    CalculateStars();
                    ProgressUpdated?.Invoke();
                }
            }
            else
            {
                Debug.LogError($"Level {levelNumber} does not exist");
            }
        }

        public bool NextLevelIsReady(int nextLevelNumber)
        {
            LevelData level = TryGetLevel(nextLevelNumber);
            if (level != null)
            {
                if (StarsEarned >= level.StarsRequired)
                {
                    return true;
                }
            }

            return false;
        }

        public LevelData TryGetLevel(int number)
        {
            return LevelsData.Where(x => x.Number == number).Select(x => x).FirstOrDefault();
        }

        private void CalculateStars()
        {
            StarsEarned = 0;
            StarsCanBeEarned = 0;
            foreach (LevelData level in LevelsData)
            {
                StarsEarned += level.StarsEarned;
                StarsCanBeEarned += StarsPerLevel;
            }
        }
    }
}