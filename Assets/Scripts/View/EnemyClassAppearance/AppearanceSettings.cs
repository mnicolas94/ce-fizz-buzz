using Core;
using UnityEngine;

namespace View.EnemyClassAppearance
{
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