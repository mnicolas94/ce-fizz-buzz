using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.TurnSteps
{
    [Serializable]
    public class DamagePlayerTurnStep : AbstractListEnemiesTurnStep
    {
        [SerializeField] private float _damage;

        public float Damage => _damage;

        public DamagePlayerTurnStep(List<Enemy> enemies, float damage) : base(enemies)
        {
            _damage = damage;
        }
    }
}