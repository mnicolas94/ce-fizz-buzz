using System.Collections;
using System.Collections.Generic;
using Core.TurnSteps;
using UnityEngine;

namespace Core.Serializables
{
    [CreateAssetMenu(fileName = "SerializableTurn", menuName = "FizzBuzz/SerializableTurn", order = 0)]
    public class SerializedTurnAsset : ScriptableObject, IList<TurnStep>
    {
        [SerializeField] private SerializableTurn _turn;

        public void AddRange(IList<TurnStep> steps)
        {
            _turn.AddRange(steps);
        }
        
        public IEnumerator<TurnStep> GetEnumerator()
        {
            return _turn.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_turn).GetEnumerator();
        }

        public void Add(TurnStep item)
        {
            _turn.Add(item);
        }

        public void Clear()
        {
            _turn.Clear();
        }

        public bool Contains(TurnStep item)
        {
            return _turn.Contains(item);
        }

        public void CopyTo(TurnStep[] array, int arrayIndex)
        {
            _turn.CopyTo(array, arrayIndex);
        }

        public bool Remove(TurnStep item)
        {
            return _turn.Remove(item);
        }

        public int Count => _turn.Count;

        public bool IsReadOnly => _turn.IsReadOnly;

        public int IndexOf(TurnStep item)
        {
            return _turn.IndexOf(item);
        }

        public void Insert(int index, TurnStep item)
        {
            _turn.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _turn.RemoveAt(index);
        }

        public TurnStep this[int index]
        {
            get => _turn[index];
            set => _turn[index] = value;
        }
    }
}