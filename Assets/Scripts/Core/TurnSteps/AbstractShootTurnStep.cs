using System.Collections.Generic;

namespace Core.TurnSteps
{
    public abstract class AbstractShootTurnStep : AbstractListEnemiesTurnStep
    {
        private EnemyClass _shotClass;

        public EnemyClass ShotClass => _shotClass;

        protected AbstractShootTurnStep(List<Enemy> enemies, EnemyClass shotClass) : base(enemies)
        {
            _shotClass = shotClass;
        }
    }
}