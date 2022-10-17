using System.Collections.Generic;
using System.Linq;
using Core;
using NUnit.Framework;

namespace Tests.EditorMode
{
    public class FizzBuzzLogicTests
    {
        [TestCase(1, EnemyClass.Dumb)]
        [TestCase(2, EnemyClass.Dumb)]
        [TestCase(3, EnemyClass.Fizz)]
        [TestCase(4, EnemyClass.Dumb)]
        [TestCase(5, EnemyClass.Buzz)]
        [TestCase(6, EnemyClass.Fizz)]
        [TestCase(7, EnemyClass.Dumb)]
        [TestCase(8, EnemyClass.Dumb)]
        [TestCase(15, EnemyClass.FizzBuzz)]
        public void GetEnemyClassFromValue_Test(int value, EnemyClass expectedEnemyClass)
        {
            // act
            var result = EnemyClassUtils.GetClassFromInt(value);

            //assert
            Assert.AreEqual(expectedEnemyClass, result);
        }
        
        [TestCase(EnemyClass.Dumb, 1, 15, 1, 2, 4, 7, 8, 11, 13, 14)]
        [TestCase(EnemyClass.Fizz, 1, 15, 3, 6, 9, 12)]
        [TestCase(EnemyClass.Buzz, 1, 15, 5, 10)]
        [TestCase(EnemyClass.FizzBuzz, 1, 15, 15)]
        public void GetValuesFromEnemyClass_Test(
            EnemyClass enemyClass,
            int rangeMin,
            int rangeMax,
            params int[] expectedValues)
        {
            // act
            var result = EnemyClassUtils.GetValuesFromClass(enemyClass, rangeMin, rangeMax).ToList();

            //assert
            Assert.AreEqual(expectedValues.Length, result.Count);
            foreach (var value in expectedValues)
            {
                Assert.IsTrue(result.Contains(value));
            }
        }
    }
}