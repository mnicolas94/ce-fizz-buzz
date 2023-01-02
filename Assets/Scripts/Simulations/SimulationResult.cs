using System.Collections.Generic;
using System.Collections.ObjectModel;
using Core.Serializables;
using UnityEngine;
using Utils.Attributes;

namespace Simulations
{
    public class SimulationResult : ScriptableObject
    {
        [SerializeField] private SimulationInputParameters _parameters;
        [SerializeField, ToStringLabel] private List<SerializableGame> _games;

        public SimulationInputParameters Parameters => _parameters;

        public ReadOnlyCollection<SerializableGame> Games => _games.AsReadOnly();

        public static SimulationResult Create(SimulationInputParameters parameters, List<SerializableGame> games)
        {
            var result = CreateInstance<SimulationResult>();
            result._parameters = parameters;
            result._games = games;
            return result;
        }
    }
}