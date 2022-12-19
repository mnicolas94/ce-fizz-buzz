using UnityEngine;

namespace Core.GameRules
{
    [CreateAssetMenu(fileName = "GameRules", menuName = "FizzBuzz/Rules/GameRules")]
    public class GameRules : ScriptableObject
    {
        [SerializeField] private SpawnRules _spawnRules = new SpawnRules();

        [SerializeField] private HealthRules _healthRules = new HealthRules();
        
        [SerializeField] private ScoreRules _scoreRules = new ScoreRules();
        
        [SerializeField] private float _moveDistancePerTurn = 1;
        
        /// <summary>
        /// Maximum distance a shot can bounce to hit other enemies
        /// </summary>
        [SerializeField] private float _distanceToBounceShot = 1;

        public SpawnRules SpawnRules
        {
            get => _spawnRules;
            set => _spawnRules = value;
        }

        public HealthRules HealthRules
        {
            get => _healthRules;
            set => _healthRules = value;
        }

        public ScoreRules ScoreRules
        {
            get => _scoreRules;
            set => _scoreRules = value;
        }

        public float MoveDistancePerTurn
        {
            get => _moveDistancePerTurn;
            set => _moveDistancePerTurn = value;
        }

        public float DistanceToBounceShot
        {
            get => _distanceToBounceShot;
            set => _distanceToBounceShot = value;
        }

        public float GetDangerRadius()
        {
            return _moveDistancePerTurn + _healthRules.DistanceToDamagePlayer;
        }
    }
}