using System;
using Core;
using Simulations.AI;
using TNRD;
using UnityEngine;

namespace Simulations
{
    [Serializable]
    public class SimulationInputParameters
    {
        [SerializeField] private GameContext _context;

        [SerializeField] private SerializableInterface<IAI> _ai;

        [SerializeField] private int _gamesCount;

        [SerializeField] private int _maxTurns;

        public GameContext Context => _context;

        public IAI AI => _ai.Value;

        public int GamesCount => _gamesCount;

        public int MaxTurns => _maxTurns;
    }
}