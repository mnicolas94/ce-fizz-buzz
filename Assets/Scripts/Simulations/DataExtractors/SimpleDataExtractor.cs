using System;
using System.Collections.Generic;
using System.Linq;
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

            var (turnsMin, turnsMax, turnsMean) = GetMinMaxMean(turnsCount);

            return new Dictionary<string, object>
            {
                { "min turns", turnsMin },
                { "max turns", turnsMax },
                { "mean turns", turnsMean },
            };
        }

        private (int, int, int) GetMinMaxMean(List<int> values)
        {
            int min = Int32.MaxValue;
            int max = Int32.MinValue;
            int mean = 0;
            foreach (var value in values)
            {
                min = Math.Min(min, value);
                max = Math.Max(max, value);
                mean += value;
            }

            mean /= values.Count;

            return (min, max, mean);
        }
        
        private (float, float, float) GetMinMaxMean(List<float> values)
        {
            float min = Int32.MaxValue;
            float max = Int32.MinValue;
            float mean = 0;
            foreach (var value in values)
            {
                min = Mathf.Min(min, value);
                max = Mathf.Max(max, value);
                mean += value;
            }

            mean /= values.Count;

            return (min, max, mean);
        }
    }
}