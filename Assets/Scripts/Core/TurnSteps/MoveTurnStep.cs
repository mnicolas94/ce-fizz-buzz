using System;
using System.Collections.Generic;

namespace Core.TurnSteps
{
    [Serializable]
    public class MoveTurnStep : AbstractListEnemiesTurnStep
    {
        public MoveTurnStep(List<Enemy> enemies) : base(enemies)
        {
        }
    }
}