using System;
using UnityEngine;

namespace Gameplay
{
    public class ScoreManager : MonoBehaviour
    {
        public event Action<int> ScoreChanged;

        public int CurrentScore { get; private set; }

        public void Add(int points)
        {
            CurrentScore += points;
            ScoreChanged?.Invoke(CurrentScore);
        }

        public void ResetScore()
        {
            CurrentScore = 0;
            ScoreChanged?.Invoke(CurrentScore);
        }
    }
}
