using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Utils.Attributes;

namespace Core
{
    /// <summary>
    /// Class that holds the variables involved in the core gameplay. 
    /// </summary>
    [CreateAssetMenu(fileName = "GameContext", menuName = "FizzBuzz/GameContext", order = 0)]
    public class GameContext : ScriptableObject
    {
        [SerializeField] private FloatVariable _playerHealth;
        [SerializeField] private FloatConstant _maxHealth;
        [SerializeField] private IntVariable _score;
        
        /// <summary>
        /// Initial and current rules of the game
        /// </summary>
        [SerializeField] private GameRules.GameRules _gameRules;
        [SerializeField, ToStringLabel] private List<ScoreRule> _difficultyScoreMilestones;

        [SerializeField, DisableIf(nameof(Always))] 
        private List<Enemy> _enemies = new List<Enemy>();

        public FloatVariable PlayerHealth
        {
            get => _playerHealth;
            set => _playerHealth = value;
        }

        public FloatConstant MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        public IntVariable Score
        {
            get => _score;
            set => _score = value;
        }

        public List<Enemy> Enemies
        {
            get => _enemies;
            set => _enemies = value;
        }

        public GameRules.GameRules GameRules
        {
            get => _gameRules;
            set => _gameRules = value;
        }

        public List<ScoreRule> DifficultyScoreMilestones
        {
            get => _difficultyScoreMilestones;
            set => _difficultyScoreMilestones = value;
        }

        private bool Always()
        {
            return true;
        }
    }

    [Serializable]
    public class ScoreRule
    {
        [SerializeField] private int _score;
        [SerializeField] private GameRules.GameRules _rules;

        public int Score => _score;

        public GameRules.GameRules Rules => _rules;

        public ScoreRule(int score, GameRules.GameRules rules)
        {
            _score = score;
            _rules = rules;
        }

        public void Deconstruct(out int score, out GameRules.GameRules rules)
        {
            score = _score;
            rules = _rules;
        }

        public override string ToString()
        {
            return $"{_score} - {_rules.name}";
        }
    }
}