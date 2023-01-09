using Core.Persistence;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace UI
{
    public class ScoreUi : MonoBehaviour
    {
        [SerializeField] private IntVariable _scoreVariable;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _recordText;
        [SerializeField] private RecordsPersistence _records;

        public void UpdateScoreUi()
        {
            _scoreText.text = $"{_scoreVariable.Value}";
            _recordText.text = $"{_records.MaxScore}";
        }
    }
}