using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace UI
{
    public class ScoreUi : MonoBehaviour
    {
        [SerializeField] private IntVariable _scoreVariable;
        [SerializeField] private TextMeshProUGUI _scoreText;

        public void UpdateScoreUi()
        {
            _scoreText.text = $"{_scoreVariable.Value}";
        }
    }
}