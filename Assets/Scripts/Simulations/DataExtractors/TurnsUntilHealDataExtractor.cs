using System.Collections.Generic;
using System.Linq;
using Core.TurnSteps;

namespace Simulations.DataExtractors
{
    public class TurnsUntilHealDataExtractor : ISimulationDataExtractor
    {
        public Dictionary<string, object> ExtractGamesData(SimulationResult simulationResult)
        {
            var games = simulationResult.Games;
            var firstHealTurnIndices = games.Select(game =>
            {
                var firstHealTurn = game.Turns.FirstOrDefault(turn =>
                {
                    bool healedThisTurn = turn.TurnSteps.Any(step => step is HealPlayerTurnStep);
                    return healedThisTurn;
                });

                var index = firstHealTurn != null ? game.Turns.IndexOf(firstHealTurn) : -1;
                return index;
            });
            firstHealTurnIndices = firstHealTurnIndices.Where(index => index != -1);
            var firstHealTurnIndicesList = firstHealTurnIndices.ToList();
            var data = "-1";
            if (firstHealTurnIndicesList.Count > 0)
            {
                data = Utils.GetMinMaxMeanString(firstHealTurnIndicesList);
            }
            return new Dictionary<string, object>
            {
                { "first heal", data }
            };
        }
    }
}