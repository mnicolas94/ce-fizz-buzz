using System;
using System.Collections.Generic;
using System.Linq;
using Core.TurnSteps;
using UnityEngine;

namespace Simulations.DataExtractors
{
    public class SimpleDataExtractor : ISimulationDataExtractor
    {
        public Dictionary<string, object> ExtractGamesData(SimulationResult simulationResult)
        {
            var games = simulationResult.Games;
            var gamesCount = games.Count;
            var turnsCount = games.Select(game => game.Turns.Count).ToList();
            var scores = games.Select(game =>
            {
                var scoreTurns = game.Turns
                    .SelectMany(turn => turn.TurnSteps)
                    .Where(step => step is ScoreChangedTurnStep)
                    .Cast<ScoreChangedTurnStep>();
                var scoreChanges = scoreTurns.Select(step => step.ScoreChange);
                var totalScore = scoreChanges.Sum();
                return totalScore;
            }).ToList();

            var (turnsMin, turnsMax, turnsMean) = Utils.GetMinMaxMean(turnsCount);
            var (scoreMin, scoreMax, scoreMean) = Utils.GetMinMaxMean(scores);

            return new Dictionary<string, object>
            {
                { "games", gamesCount },
                { "min turns", turnsMin },
                { "max turns", turnsMax },
                { "mean turns", turnsMean },
                { "min score", scoreMin },
                { "max score", scoreMax },
                { "mean score", scoreMean },
            };
        }
    }
}