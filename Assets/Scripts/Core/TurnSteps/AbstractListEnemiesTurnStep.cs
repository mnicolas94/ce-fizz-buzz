using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.TurnSteps
{
    [Serializable]
    public abstract class AbstractListEnemiesTurnStep : TurnStep
    {
        [SerializeField] private List<Enemy> _enemies;

        public List<Enemy> Enemies => _enemies;

        protected AbstractListEnemiesTurnStep(List<Enemy> enemies)
        {
            _enemies = enemies;
        }
    }
}