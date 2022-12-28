using System;
using Core;
using Utils.Extensions;

namespace Simulations.AI
{
    [Serializable]
    public class RandomAI : IAI
    {
        public (Enemy, EnemyClass) InferAction(GameContext context)
        {
            var enemy = context.Enemies.GetRandom();
            var clazz = EnemyClassUtils.GetRandomClass();
            return (enemy, clazz);
        }
    }
}