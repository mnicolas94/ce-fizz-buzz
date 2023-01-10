using System.Collections.Generic;
using System.Linq;
using Core.TurnSteps;

namespace Simulations.DataExtractors
{
    public class HealPerTurnDataExtractor : ISimulationDataExtractor
    {
        public Dictionary<string, object> ExtractGamesData(SimulationResult simulationResult)
        {
            var games = simulationResult.Games;
            var allTurnsHeal = games.SelectMany(game =>
            {
                var turnsHeal = game.Turns.Select(turn =>
                {
                    var healReceived = turn.TurnSteps
                        .Where(step => step is HealPlayerTurnStep)
                        .Cast<HealPlayerTurnStep>()
                        .Select(step => step.Heal)
                        .Sum();
                    return healReceived;
                });
                return turnsHeal;
            });
            
            var healPerTurn = Utils.GetMinMaxMeanString(allTurnsHeal.ToList());
            return new Dictionary<string, object>
            {
                { "heal per turn", healPerTurn }
            };
        }
    }
}