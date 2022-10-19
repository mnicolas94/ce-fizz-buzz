using System.Collections.Generic;

namespace Core.TurnSteps
{
    public class DamagePlayerTurnStep : AbstractListEnemiesTurnStep
    {
        private float _damage;

        public float Damage => _damage;

        public DamagePlayerTurnStep(List<Enemy> enemies, float damage) : base(enemies)
        {
            _damage = damage;
        }
    }
}