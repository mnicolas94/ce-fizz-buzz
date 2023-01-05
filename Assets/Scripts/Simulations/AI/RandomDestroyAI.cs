using System;
using Core;
using Utils.Extensions;

namespace Simulations.AI
{
    [Serializable]
    public class RandomDestroyAI : IAI
    {
        public (Enemy, EnemyClass) InferAction(GameContext context)
        {
            var enemy = context.Enemies.GetRandom();
            var shotClass = enemy.CurrentClass;
            return (enemy, shotClass);
        }
    }
}