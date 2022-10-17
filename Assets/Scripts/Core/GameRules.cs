using System;
using NaughtyAttributes;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "GameRules", menuName = "FizzBuzz/GameRules")]
    public class GameRules : ScriptableObject
    {
        /// <summary>
        /// Minimum number an enemy will be spawned with
        /// </summary>
        [SerializeField, Foldout("Spawning")] private int _minNumber;
        /// <summary>
        /// Maximum number an enemy will be spawned with
        /// </summary>
        [SerializeField, Foldout("Spawning")] private int _maxNumber;

        [SerializeField, Foldout("Spawning")] private float _distanceToSpawnEnemy;
        
        [SerializeField, Foldout("Player health")] private float _distanceToDamagePlayer;
        
        [SerializeField, Foldout("Player health")] private float _damagePerHit; 
        
        [SerializeField] private float _moveDistancePerTurn;
        
        /// <summary>
        /// Maximum distance a shot can bounce to hit other enemies
        /// </summary>
        [SerializeField] private float _distanceToBounceShot;

        public int MinNumber => _minNumber;

        public int MaxNumber => _maxNumber;

        public float MoveDistancePerTurn => _moveDistancePerTurn;

        public float DistanceToDamagePlayer => _distanceToDamagePlayer;

        public float DamagePerHit => _damagePerHit;

        public float DistanceToSpawnEnemy => _distanceToSpawnEnemy;

        public float DistanceToBounceShot => _distanceToBounceShot;

        public static GameRules Create(
            int minNumber = 1,
            int maxNumber = 15,
            float moveDistancePerTurn = 1,
            float distanceToDamagePlayer = 1,
            float distanceToSpawnEnemy = 5.5f,
            float distanceToBounceShot = 1)
        {
            var rules = CreateInstance<GameRules>();
            rules._minNumber = minNumber;
            rules._maxNumber = maxNumber;
            rules._moveDistancePerTurn = moveDistancePerTurn;
            rules._distanceToDamagePlayer = distanceToDamagePlayer;
            rules._distanceToSpawnEnemy = distanceToSpawnEnemy;
            rules._distanceToBounceShot = distanceToBounceShot;
            return rules;
        }
    }
}