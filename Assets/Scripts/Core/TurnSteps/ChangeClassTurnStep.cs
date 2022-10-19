using System.Collections.Generic;

namespace Core.TurnSteps
{
    public class ChangeClassTurnStep : AbstractShootTurnStep
    {
        public ChangeClassTurnStep(List<Enemy> enemies, EnemyClass shotClass) : base(enemies, shotClass)
        {
        }
    }
}