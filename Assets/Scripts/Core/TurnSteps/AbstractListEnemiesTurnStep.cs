using System.Collections.Generic;

namespace Core.TurnSteps
{
    public abstract class AbstractListEnemiesTurnStep : TurnStep
    {
        private List<Enemy> _enemies;

        public List<Enemy> Enemies => _enemies;

        protected AbstractListEnemiesTurnStep(List<Enemy> enemies)
        {
            _enemies = enemies;
        }
    }
}