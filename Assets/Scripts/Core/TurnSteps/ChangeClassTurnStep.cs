using System;
using System.Collections.Generic;

namespace Core.TurnSteps
{
    [Serializable]
    public class ChangeClassTurnStep : AbstractShootTurnStep
    {
        public ChangeClassTurnStep(List<Enemy> enemies, EnemyClass shotClass) : base(enemies, shotClass)
        {
        }
    }
}