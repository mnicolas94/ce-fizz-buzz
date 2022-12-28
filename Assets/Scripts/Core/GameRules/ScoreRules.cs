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
        [SerializeField] private ClassScoreDictionary _classesScore = new ClassScoreDictionary();

        public int GetEnemyScore(Enemy enemy)
        {
            var enemyClass = enemy.CurrentClass;
            if (_classesScore.ContainsKey(enemyClass))
                return _classesScore[enemyClass];

            return 1;
        }

        public void SetEnemyClassScore(EnemyClass clazz, int score)
        {
            _classesScore[clazz] = score;
        }
    }
}