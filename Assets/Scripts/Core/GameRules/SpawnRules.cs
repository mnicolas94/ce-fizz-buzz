using System;
using UnityEngine;

namespace Core.GameRules
{
    [Serializable]
    public class SpawnRules
    {
        /// <summary>
        /// Minimum number an enemy will be spawned with
        /// </summary>
        [SerializeField] private int _minNumber = 1;
        /// <summary>
        /// Maximum number an enemy will be spawned with
        /// </summary>
        [SerializeField] private int _maxNumber = 15;

        [SerializeField] private float _distanceToSpawnEnemy = 5.5f;


        public int MinNumber
        {
            get => _minNumber;
            set => _minNumber = value;
        }

        public int MaxNumber
        {
            get => _maxNumber;
            set => _maxNumber = value;
        }

        public float DistanceToSpawnEnemy
        {
            get => _distanceToSpawnEnemy;
            set => _distanceToSpawnEnemy = value;
        }
    }
}