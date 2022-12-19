using System;
using UnityEngine;
using Utils.Serializables;

namespace Core.GameRules
{
    [Serializable]
    public class ClassScoreDictionary : SerializableDictionary<EnemyClass, int>{}
    
    [Serializable]
    public class ScoreRules
    {
        [SerializeField] private ClassScoreDictionary _classesScore;

        public int GetEnemyScore(Enemy enemy)
        {
            return _classesScore[enemy.CurrentClass];
        }
    }
}