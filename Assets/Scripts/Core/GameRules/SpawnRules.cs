using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GameRules
{
    [Serializable]
    public class SpawnRules
    {
        [SerializeField, Tooltip("Min number of enemies to spawn per turn.")]
        private int _minSpawnCount = 1;
        
        [SerializeField, Tooltip("Max number of enemies to spawn per turn.")]
        private int _maxSpawnCount = 1;
        
        [SerializeField, Tooltip("Minimum number an enemy will be spawned with.")]
        private int _minNumber = 1;
        
        [SerializeField, Tooltip("Maximum number an enemy will be spawned with.")]
        private int _maxNumber = 15;

        [SerializeField] private float _distanceToSpawnEnemy = 5.5f;

        [SerializeField, Tooltip("Amount of circular sectors to spawn enemies separated of each other and, thus, avoid overlapping.")]
        private int _circularSectors = 18;


        public int MinSpawnCount
        {
            get => _minSpawnCount;
            set => _minSpawnCount = value;
        }

        public int MaxSpawnCount
        {
            get => _maxSpawnCount;
            set => _maxSpawnCount = value;
        }

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

        public int CircularSectors
        {
            get => _circularSectors;
            set => _circularSectors = value;
        }

        public float DistanceToSpawnEnemy
        {
            get => _distanceToSpawnEnemy;
            set => _distanceToSpawnEnemy = value;
        }

        public List<float> GetAvailableSpawnAngles()
        {
            var angles = new List<float>();
            var angleOffset = 360 / _circularSectors;

            for (int i = 0; i < _circularSectors; i++)
            {
                angles.Add(i * angleOffset);
            }

            return angles;
        }
    }
}