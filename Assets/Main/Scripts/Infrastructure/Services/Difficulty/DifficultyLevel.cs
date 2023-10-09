using System;

namespace Main.Scripts.Infrastructure.Services.Difficulty
{
    [Serializable]
    public class DifficultyLevel
    {
        public int BlockCount;
        public float Frequency;
    }
}