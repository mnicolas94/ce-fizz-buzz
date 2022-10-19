using System;
using UnityEngine;

namespace Core.TurnSteps
{
    [Serializable]
    public class ScoreChangedTurnStep : TurnStep
    {
        [SerializeField] private int _scoreChange;

        public int ScoreChange => _scoreChange;

        public ScoreChangedTurnStep(int scoreChange)
        {
            _scoreChange = scoreChange;
        }
    }
}