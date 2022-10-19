using System.Collections.Generic;

namespace Core.TurnSteps
{
    public class SpawnTurnStep : AbstractListEnemiesTurnStep
    {
        public SpawnTurnStep(List<Enemy> enemies) : base(enemies)
        {
        }
    }
}