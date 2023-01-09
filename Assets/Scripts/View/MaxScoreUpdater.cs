using Core.Persistence;
using SaveSystem;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace View
{
    public class MaxScoreUpdater : MonoBehaviour
    {
        [SerializeField] private IntVariable _scoreVariable;
        [SerializeField] private RecordsPersistence _records;

        private async void Start()
        {
            await _records.LoadOrCreate();
            _scoreVariable.Changed.Register(OnScoreChanged);
        }

        private void OnScoreChanged(int newScore)
        {
            _records.SetNewMaxScore(newScore);
            _records.Save();
        }
    }
}