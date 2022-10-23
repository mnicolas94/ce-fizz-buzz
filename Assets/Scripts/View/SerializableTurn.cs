using System.Collections;
using System.Collections.Generic;
using Core.TurnSteps;
using UnityEngine;

namespace View
{
    [CreateAssetMenu(fileName = "SerializableTurn", menuName = "FizzBuzz/SerializableTurn", order = 0)]
    public class SerializableTurn : ScriptableObject, IList<TurnStep>
    {
        [SerializeReference] private List<TurnStep> _turnSteps;

        public void AddRange(IList<TurnStep> other)
        {
            foreach (var turnStep in other)
            {
                Add(turnStep);
            }
        }
        
#region IList implementation

        public IEnumerator<TurnStep> GetEnumerator()
        {
            return _turnSteps.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _turnSteps).GetEnumerator();
        }

        public void Add(TurnStep item)
        {
            _turnSteps.Add(item);
        }

        public void Clear()
        {
            _turnSteps.Clear();
        }

        public bool Contains(TurnStep item)
        {
            return _turnSteps.Contains(item);
        }

        public void CopyTo(TurnStep[] array, int arrayIndex)
        {
            _turnSteps.CopyTo(array, arrayIndex);
        }

        public bool Remove(TurnStep item)
        {
            return _turnSteps.Remove(item);
        }

        public int Count => _turnSteps.Count;

        public bool IsReadOnly => ((ICollection<TurnStep>) _turnSteps).IsReadOnly;

        public int IndexOf(TurnStep item)
        {
            return _turnSteps.IndexOf(item);
        }

        public void Insert(int index, TurnStep item)
        {
            _turnSteps.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _turnSteps.RemoveAt(index);
        }

        public TurnStep this[int index]
        {
            get => _turnSteps[index];
            set => _turnSteps[index] = value;
        }
        
#endregion
    }
}