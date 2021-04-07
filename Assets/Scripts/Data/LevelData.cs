using System;
using StaticData;

namespace Data
{
    [Serializable]
    public class LevelData
    {
        public int Number;
        public LevelId LevelId;
        public int StarsRequired;
        public int StarsEarned;
    }
}