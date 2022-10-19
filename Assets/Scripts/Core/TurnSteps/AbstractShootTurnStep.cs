using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.TurnSteps
{
    [Serializable]
    public abstract class AbstractShootTurnStep : AbstractListEnemiesTurnStep
    {
        [SerializeField] private EnemyClass _shotClass;

        public EnemyClass ShotClass => _shotClass;

        protected AbstractShootTurnStep(List<Enemy> enemies, EnemyClass shotClass) : base(enemies)
        {
            _shotClass = shotClass;
        }
    }
}