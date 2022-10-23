using System;
using UnityEngine;

namespace Core.TurnSteps
{
    [Serializable]
    public class HealPlayerTurnStep : TurnStep
    {
        [SerializeField] private float _heal;

        public float Heal => _heal;

        public HealPlayerTurnStep(float heal)
        {
            _heal = heal;
        }
    }
}