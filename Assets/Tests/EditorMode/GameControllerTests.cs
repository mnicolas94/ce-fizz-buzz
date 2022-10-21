using System.Collections;
using System.Collections.Generic;
using Core;
using Core.GameRules;
using Core.TurnSteps;
using NUnit.Framework;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.EditorMode
{
    public class GameControllerTests
    {
        [Test]
        public void WhenGameStarts_SpawnAndMoveTurnStepsOccur()
        {
            // arrange
            var rules = ScriptableObject.CreateInstance<GameRules>();
            var gameController = TestsUtils.GetGameController(rules: rules);

            // act
            var turnSteps = gameController.StartGame();

            // assert
            Assert.IsTrue(turnSteps.Exists(step => step is MoveTurnStep));
            Assert.IsTrue(turnSteps.Exists(step => step is SpawnTurnStep));
        }
        
        [Test]
        public void WhenGameStarts_HealthIsCorrectlyInitialized()
        {
            // arrange
            var playerHealth = ScriptableObject.CreateInstance<FloatVariable>();
            var maxHealth = TestsUtils.GetTestMaxHealthConstant();
            // maxHealth.Value = expectedValue;  // this can't be done because is a constant
            var expectedValue = maxHealth.Value;
            var gameController = TestsUtils.GetGameController(maxHealth: maxHealth, playerHealth: playerHealth);

            // act
            gameController.StartGame();

            // assert
            Assert.AreEqual(expectedValue, playerHealth.Value);
        }
        
        [Test]
        public void WhenGameStarts_ScoreIsCorrectlyInitialized()
        {
            // arrange
            var score = ScriptableObject.CreateInstance<IntVariable>();
            score.Value = 150;
            var gameController = TestsUtils.GetGameController(scoreVariable: score);
            var expected = 0;

            // act
            gameController.StartGame();

            // assert
            Assert.AreEqual(expected, score.Value);
        }

        [Test]
        public void WhenShootEnemyWithSameClassShot_DestroyStepOccur()
        {
            // arrange
            var enemy = new Enemy(Vector2.right, 1);
            var enemies = new List<Enemy>
            {
                enemy
            };
            var gameController = TestsUtils.GetGameController(enemies: enemies);
            var expectedShotClass = EnemyClass.Dumb;

            // act
            var turnSteps = gameController.PlayTurn(enemy, expectedShotClass);

            // assert
            Assert.IsTrue(turnSteps.Exists(step => step is DestroyTurnStep));
            var destroyStep = turnSteps.Find(step => step is DestroyTurnStep) as DestroyTurnStep;
            Assert.Contains(enemy, destroyStep.Enemies);
            Assert.IsTrue(destroyStep.ShotClass == expectedShotClass);
        }
        
        [TestCase(4, 4)]
        [TestCase(3, 3, 2)]
        [TestCase(1, 1, 3, 5, 15)]
        [TestCase(6, 2, 4, 6)]  // with bounce
        [TestCase(15, 15, 1, 2, 3, 4, 5, 6)]
        public void WhenShootEnemyWithSameClassShot_ScoreStepOccur(int expectedScore, params int[] numbers)
        {
            // arrange
            var rules = ScriptableObject.CreateInstance<GameRules>();
            rules.DistanceToBounceShot = 1;
            var enemies = new List<Enemy>();
            foreach (var number in numbers)
            {
                var enemy = new Enemy(Vector2.right * 5, number);
                enemies.Add(enemy);
            }
            
            var gameController = TestsUtils.GetGameController(rules: rules, enemies: enemies);
            var firstEnemy = enemies[0];
            var enemyClass = firstEnemy.CurrentClass;

            // act
            var turnSteps = gameController.PlayTurn(firstEnemy, enemyClass);

            // assert
            Assert.IsTrue(turnSteps.Exists(step => step is ScoreChangedTurnStep));
            var scoreStep = turnSteps.Find(step => step is ScoreChangedTurnStep) as ScoreChangedTurnStep;
            Assert.AreEqual(expectedScore, scoreStep.ScoreChange);
        }
        
        [Test]
        public void WhenShootEnemyWithDifferentClassShot_ChangeClassStepOccur()
        {
            // arrange
            var enemy = new Enemy(Vector2.right, 1);
            var enemies = new List<Enemy>
            {
                enemy
            };
            var gameController = TestsUtils.GetGameController(enemies: enemies);
            var expectedShotClass = EnemyClass.Fizz;

            // act
            var turnSteps = gameController.PlayTurn(enemy, expectedShotClass);

            // assert
            Assert.IsTrue(turnSteps.Exists(step => step is ChangeClassTurnStep));
            var changeStep = turnSteps.Find(step => step is ChangeClassTurnStep) as ChangeClassTurnStep;
            Assert.Contains(enemy, changeStep.Enemies);
            Assert.IsTrue(changeStep.ShotClass == expectedShotClass);
        }
        
        [Test]
        public void WhenEnemiesGetTooClose_PlayerGetsDamaged()
        {
            // arrange
            var rules = ScriptableObject.CreateInstance<GameRules>();
            rules.HealthRules.DamagePerHit = 1;
            rules.HealthRules.DistanceToDamagePlayer = 1;
            var enemy1 = new Enemy(new Vector2(1.1f, 0), 1);
            var enemy2 = new Enemy(new Vector2(0, 1.8f), 1);
            var enemy3 = new Enemy(new Vector2(-2.1f, 0), 1);
            var enemy4 = new Enemy(new Vector2(0, -1.3f), 1);
            var enemies = new List<Enemy>
            {
                enemy1,
                enemy2,
                enemy3,
                enemy4,
            };
            var gameController = TestsUtils.GetGameController(rules: rules, enemies: enemies);
            var expectedDamage = 3;

            // act
            var turnSteps = gameController.PlayTurn(enemy1, EnemyClass.Fizz);  // do not destroy

            // assert
            Assert.IsTrue(turnSteps.Exists(step => step is DamagePlayerTurnStep));
            var damageStep = turnSteps.Find(step => step is DamagePlayerTurnStep) as DamagePlayerTurnStep;
            Assert.AreEqual(damageStep.Damage, expectedDamage);
            Assert.Contains(enemy1, damageStep.Enemies);
            Assert.Contains(enemy2, damageStep.Enemies);
            Assert.Contains(enemy4, damageStep.Enemies);
            Assert.IsFalse(damageStep.Enemies.Contains(enemy3));
        }
    }
}