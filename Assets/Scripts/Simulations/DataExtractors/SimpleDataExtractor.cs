using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Serializables;
using Core.TurnSteps;
using UnityEngine;

namespace Simulations.DataExtractors
{
    [Serializable]
    public class SimpleDataExtractor : ISimulationDataExtractor
    {
        public Dictionary<string, object> ExtractGamesData(SimulationResult simulationResult)
        {
            var games = simulationResult.Games;
            var gamesCount = games.Count;
            var turnsCount = games.Select(game => game.Turns.Count).ToList();
            var scoresPerGame = GetScorePerGames(games);
            var scoresPerTurns = GetScorePerTurns(games);

            var mmmTurnsPerGame = Utils.GetMinMaxMeanString(turnsCount);
            var mmmScorePerGames = Utils.GetMinMaxMeanString(scoresPerGame);
            var mmmScorePerTurns = Utils.GetMinMaxMeanString(scoresPerTurns);
            
            return new Dictionary<string, object>
            {
                { "games", gamesCount },
                { "turns", mmmTurnsPerGame },
                { "score (per game)", mmmScorePerGames },
                { "score (per turn)", mmmScorePerTurns },
            };
        }

        private static List<int> GetScorePerGames(ReadOnlyCollection<SerializableGame> games)
        {
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
            return scores;
        }
        
        private static List<int> GetScorePerTurns(ReadOnlyCollection<SerializableGame> games)
        {
            var scores = games.SelectMany(game =>
            {
                var scoreTurns = game.Turns
                    .SelectMany(turn => turn.TurnSteps)
                    .Where(step => step is ScoreChangedTurnStep)
                    .Cast<ScoreChangedTurnStep>();
                var scoreChanges = scoreTurns.Select(step => step.ScoreChange);
                return scoreChanges;
            }).ToList();
            return scores;
        }
    }
}