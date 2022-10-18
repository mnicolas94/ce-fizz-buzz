using System.Collections;
using Core;
using Core.GameRules;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests.EditorMode
{
    public class SpawningTests
    {
        [TestCase(2, 0, 180)]
        [TestCase(4, 0, 90, 180, 270)]
        [TestCase(10, 0, 36, 72, 108, 144, 180, 216, 252, 288, 324)]
        public void GetAvailableSpawnAngles_Test(int circularSectors, params float[] expectedAngles)
        {
            // arrange
            var spawnRules = new SpawnRules();
            spawnRules.CircularSectors = circularSectors;

            // act
            var result = spawnRules.GetAvailableSpawnAngles();

            // assert
            Assert.AreEqual(expectedAngles.Length, result.Count);
            foreach (var expectedAngle in expectedAngles)
            {
                Assert.IsTrue(result.Contains(expectedAngle));
            }
        }

        [Test]
        public void WhenSpawnEnemies_TheCorrectCountIsReturned()
        {
            // arrange
            var expectedCount = 5;
            var spawnRules = new SpawnRules();
            spawnRules.MinSpawnCount = expectedCount;
            spawnRules.MaxSpawnCount = expectedCount;
            
            // act
            var newEnemies = GameMechanics.SpawnEnemies(spawnRules);
            
            // arrange
            Assert.AreEqual(expectedCount, newEnemies.Count);
        }
        
        [Test]
        public void WhenSpawnEnemies_NoMoreThanCircularSectorsCountAreSpawned()
        {
            // arrange
            var expectedCount = 10;
            var spawnRules = new SpawnRules();
            spawnRules.CircularSectors = expectedCount;
            spawnRules.MinSpawnCount = expectedCount + 1;
            spawnRules.MaxSpawnCount = expectedCount + 1;
            
            // act
            var newEnemies = GameMechanics.SpawnEnemies(spawnRules);
            
            // arrange
            Assert.LessOrEqual(expectedCount, newEnemies.Count);
        }
    }
}