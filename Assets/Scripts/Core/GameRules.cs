using System;
using UnityEngine;

namespace Core
{
    public class GameRules : ScriptableObject
    {
        /// <summary>
        /// Minimum number an enemy will be generated with
        /// </summary>
        [SerializeField] private int _minNumber;
        /// <summary>
        /// Maximum number an enemy will be generated with
        /// </summary>
        [SerializeField] private int _maxNumber;

        [SerializeField] private float _moveDistancePerTurn;

        [SerializeField] private float _distanceToDamagePlayer;

        [SerializeField] private float _distanceToSpawnEnemy;
        
        /// <summary>
        /// Maximum distance a shot can bounce to hit other enemies
        /// </summary>
        [SerializeField] private float _distanceToBounceShot;

        public int MinNumber => _minNumber;

        public int MaxNumber => _maxNumber;

        public float MoveDistancePerTurn => _moveDistancePerTurn;

        public float DistanceToDamagePlayer => _distanceToDamagePlayer;

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