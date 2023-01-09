using UnityEngine;

namespace Core.Persistence
{
    [CreateAssetMenu(fileName = "RecordsPersistence", menuName = "RecordsPersistence", order = 0)]
    public class RecordsPersistence : ScriptableObject
    {
        [SerializeField] private int _maxScore;

        public int MaxScore => _maxScore;

        public void SetNewMaxScore(int newScore)
        {
            if (newScore > _maxScore)
            {
                _maxScore = newScore;
            }
        }
    }
}