using System.Collections.Generic;

namespace Core.TurnSteps
{
    public class DestroyTurnStep : AbstractShootTurnStep
    {
        public DestroyTurnStep(List<Enemy> enemies, EnemyClass shotClass) : base(enemies, shotClass)
        {
        }
    }
}