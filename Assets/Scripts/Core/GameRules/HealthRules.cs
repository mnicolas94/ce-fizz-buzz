using System;
using UnityEngine;

namespace Core.GameRules
{
    [Serializable]
    public class HealthRules
    {
        [SerializeField] private float _distanceToDamagePlayer = 1;
        
        [SerializeField] private float _damagePerHit = 1;

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
    }
}