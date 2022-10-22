using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "GameContext", menuName = "FizzBuzz/GameContext", order = 0)]
    public class GameContext : ScriptableObject
    {
        [SerializeField] private FloatVariable _playerHealth;
        [SerializeField] private FloatConstant _maxHealth;
        [SerializeField] private IntVariable _score;
        [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
        [SerializeField] private GameRules.GameRules _gameRules;

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
    }
}