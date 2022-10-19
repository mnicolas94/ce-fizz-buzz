using System;
using System.Collections.Generic;

namespace Core.TurnSteps
{
    [Serializable]
    public class SpawnTurnStep : AbstractListEnemiesTurnStep
    {
        public SpawnTurnStep(List<Enemy> enemies) : base(enemies)
        {
        }
    }
}