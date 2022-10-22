using System;
using UnityEngine;

namespace Core.GameRules
{
    [Serializable]
    public class HealthRules
    {
        [SerializeField] private float _distanceToDamagePlayer = 1;
        
        [SerializeField] private float _damagePerHit = 1;

        [SerializeField] private int _healPlayerOnScoreMultiplier = 100;
        [SerializeField] private int _healAmount = 1;

        public float DistanceToDamagePlayer
        {
            get => _distanceToDamagePlayer;
            set => _distanceToDamagePlayer = value;
        }

        public float DamagePerHit
        {
            get => _damagePerHit;
            set => _damagePerHit = value;
        }

        public int HealPlayerOnScoreMultiplier
        {
            get => _healPlayerOnScoreMultiplier;
            set => _healPlayerOnScoreMultiplier = value;
        }

        public int HealAmount
        {
            get => _healAmount;
            set => _healAmount = value;
        }
    }
}