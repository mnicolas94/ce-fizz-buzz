using System;
using UnityEngine;

namespace Core.TurnSteps
{
    [Serializable]
    public class HealPlayerTurnStep : TurnStep
    {
        [SerializeField] private float _heal;
        [SerializeField] private float _currentHealth;

        public float Heal => _heal;

        public float CurrentHealth => _currentHealth;

        public HealPlayerTurnStep(float heal, float currentHealth)
        {
            _heal = heal;
            _currentHealth = currentHealth;
        }
    }
}