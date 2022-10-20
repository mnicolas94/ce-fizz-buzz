using System;
using Core;
using UnityEngine;
using Utils.Serializables;

namespace View.EnemyClassAppearance
{
    [Serializable]
    public class ClassAppearanceDictionary : SerializableDictionary<EnemyClass, ClassAppearance>{}
    
    [CreateAssetMenu(fileName = "AppearanceSettings", menuName = "FizzBuzz/AppearanceSettings", order = 0)]
    public class AppearanceSettings : ScriptableObject
    {
        [SerializeField] private ClassAppearanceDictionary _appearances;

        public ClassAppearance GetAppearance(EnemyClass enemyClass)
        {
            return _appearances[enemyClass];
        }
    }
}