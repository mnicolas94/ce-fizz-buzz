using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Utils.Attributes;

namespace Core.Serializables
{
    [Serializable]
    public class SerializableGame
    {
        [SerializeField] private GameContext _context;
        [SerializeField, ToStringLabel] private List<SerializableTurn> _turns;

        public ReadOnlyCollection<SerializableTurn> Turns => _turns.AsReadOnly();

        public SerializableGame(GameContext context)
        {
            _context = context;
            _turns = new List<SerializableTurn>();
        }

        public void AddTurn(SerializableTurn turn)
        {
            _turns.Add(turn);
        }
        
        public override string ToString()
        {
            return $"Game: {_turns.Count} turns";
        }
    }
}