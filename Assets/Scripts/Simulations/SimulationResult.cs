using System.Collections.Generic;
using Core.Serializables;
using UnityEngine;
using Utils.Attributes;

namespace Simulations
{
    public class SimulationResult : ScriptableObject
    {
        [SerializeField] private SimulationInputParameters _parameters;
        [SerializeField, ToStringLabel] private List<SerializableGame> _games;

        public static SimulationResult Create(SimulationInputParameters parameters, List<SerializableGame> games)
        {
            var result = CreateInstance<SimulationResult>();
            result._parameters = parameters;
            result._games = games;
            return result;
        }
    }
}