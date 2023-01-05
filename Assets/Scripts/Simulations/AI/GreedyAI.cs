using System.Linq;
using Core;

namespace Simulations.AI
{
    public class GreedyAI : IAI
    {
        public (Enemy, EnemyClass) InferAction(GameContext context)
        {
            var enemies = context.Enemies;

            var scores = enemies.Select(enemy => (enemy, ScoreEnemy(context, enemy)));
            Enemy maxEnemy = null;
            int maxScore = 0;
            foreach (var (enemy, score) in scores)
            {
                if (score > maxScore)
                {
                    maxEnemy = enemy;
                    maxScore = score;
                }
            }
            
            return (maxEnemy, maxEnemy.CurrentClass);
        }

        private int ScoreEnemy(GameContext context, Enemy enemy)
        {
            var sequence = GameMechanics.GetShotBounceSequence(enemy, context.Enemies, context.GameRules);
            var scores = sequence.Select(enm => context.GameRules.ScoreRules.GetEnemyScore(enm));
            var totalScore = scores.Sum();
            return totalScore;
        }
    }
}