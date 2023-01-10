using System.Collections.Generic;
using System.Linq;
using Core.TurnSteps;

namespace Simulations.DataExtractors
{
    public class DamagePerTurnDataExtractor : ISimulationDataExtractor
    {
        public Dictionary<string, object> ExtractGamesData(SimulationResult simulationResult)
        {
            var games = simulationResult.Games;
            var allTurnsDamage = games.SelectMany(game =>
            {
                var turnsDamage = game.Turns.Select(turn =>
                {
                    var damageReceived = turn.TurnSteps
                        .Where(step => step is DamagePlayerTurnStep)
                        .Cast<DamagePlayerTurnStep>()
                        .Select(step => step.Damage)
                        .Sum();
                    return damageReceived;
                });
                return turnsDamage;
            });
            
            var damagePerTurn = Utils.GetMinMaxMeanString(allTurnsDamage.ToList());
            return new Dictionary<string, object>
            {
                { "damage per turn", damagePerTurn }
            };
        }
    }
}