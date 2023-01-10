using System.Collections.Generic;
using System.Linq;
using Core.TurnSteps;

namespace Simulations.DataExtractors
{
    public class TurnsUntilDamageDataExtractor : ISimulationDataExtractor
    {
        public Dictionary<string, object> ExtractGamesData(SimulationResult simulationResult)
        {
            var games = simulationResult.Games;
            var firstDamageTurnIndices = games.Select(game =>
            {
                var firstDamageTurn = game.Turns.First(turn =>
                {
                    bool damageThisTurn = turn.TurnSteps.Any(step => step is DamagePlayerTurnStep);
                    return damageThisTurn;
                });

                var index = game.Turns.IndexOf(firstDamageTurn);
                return index;
            });
            var data = Utils.GetMinMaxMeanString(firstDamageTurnIndices.ToList());
            return new Dictionary<string, object>
            {
                { "first damage", data }
            };
        }
    }
}