using System.Collections.Generic;

namespace Core.TurnSteps
{
    public class MoveTurnStep : AbstractListEnemiesTurnStep
    {
        public MoveTurnStep(List<Enemy> enemies) : base(enemies)
        {
        }
    }
}