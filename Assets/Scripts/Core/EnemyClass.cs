using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public enum EnemyClass
    {
        Dumb,
        Fizz,
        Buzz,
        FizzBuzz
    }

    public static class EnemyClassUtils
    {
        /// <summary>
        /// Get enemy class from an integer value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static EnemyClass GetClassFromInt(int value)
        {
            bool multiple3 = value % 3 == 0;
            bool multiple5 = value % 5 == 0;

            EnemyClass enemyClass = EnemyClass.Dumb;
            
            if (multiple3 && multiple5)
            {
                enemyClass = EnemyClass.FizzBuzz;
            }
            else if (multiple5)
            {
                enemyClass = EnemyClass.Buzz;
            }
            else if (multiple3)
            {
                enemyClass = EnemyClass.Fizz;
            }

            return enemyClass;
        }

        /// <summary>
        /// Get all valid integer values in a range for a specified enemy class.
        /// </summary>
        /// <param name="enemyClass"></param>
        /// <param name="rangeMin"></param>
        /// <param name="rangeMax"></param>
        /// <returns></returns>
        public static IEnumerable<int> GetValuesFromClass(EnemyClass enemyClass, int rangeMin, int rangeMax)
        {
            // ensure the range values are correctly sorted
            var min = Math.Min(rangeMin, rangeMax);
            var max = Math.Max(rangeMin, rangeMax);
            
            for (int i = min; i <= max; i++)
            {
                var @class = GetClassFromInt(i);
                if (enemyClass == @class)
                {
                    yield return i;
                }
            }
        }
    }
}