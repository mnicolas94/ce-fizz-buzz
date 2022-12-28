using Core;

namespace Simulations.AI
{
    public interface IAI
    {
        (Enemy, EnemyClass) InferAction(GameContext context);
    }
}