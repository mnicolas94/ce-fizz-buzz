using System;
using System.Collections.Generic;
using TNRD;
using UnityEngine;

namespace Simulations.DataExtractors
{
    [Serializable]
    public class DataExtractorComposite : ISimulationDataExtractor
    {
        [SerializeField] private List<SerializableInterface<ISimulationDataExtractor>> _extractors;

        public Dictionary<string, object> ExtractGamesData(SimulationResult simulationResult)
        {
            var result = new Dictionary<string, object>();
            foreach (var extractor in _extractors)
            {
                var data = extractor.Value.ExtractGamesData(simulationResult);
                UpdateDict(result, data);
            }

            return result;
        }

        public void UpdateDict(Dictionary<string, object> original, Dictionary<string, object> update)
        {
            foreach (var key in update.Keys)
            {
                original[key] = update[key];
            }
        }
    }
}