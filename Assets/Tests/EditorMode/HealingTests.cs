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
    public class HealingTests
    {
        [TestCase(4, 10, 6, EnemyClass.Fizz)]
        [TestCase(4, 10, 7, EnemyClass.Dumb)]
        [TestCase(9, 10, 12, EnemyClass.Fizz)]
        public void WhenScoreMultiplierReached_HealStepOccurs(
            int initScore,
            int scoreMultiplier,
            int enemyScore,
            EnemyClass shotClass
        )
        {
            // arrange
            var score = ScriptableObject.CreateInstance<IntVariable>();
            score.Value = initScore;
            
            var rules = ScriptableObject.CreateInstance<GameRules>();
            rules.HealthRules.HealPlayerOnScoreMultiplier = scoreMultiplier;
            
            var enemy = new Enemy(new Vector2(1, 0), enemyScore);
            var enemies = new List<Enemy>
            {
                enemy,
            };
            var gameController = TestsUtils.GetGameController(
                scoreVariable: score,
                rules: rules,
                enemies: enemies);

            // act
            var turnSteps = gameController.PlayTurn(enemy, shotClass);

            // assert
            Assert.IsTrue(turnSteps.Exists(step => step is HealPlayerTurnStep));
        }
        
        [TestCase(9, 10, 1, 4, 10, 6, EnemyClass.Fizz)]
        [TestCase(9, 10, 1, 4, 10, 7, EnemyClass.Dumb)]
        [TestCase(9, 11, 1, 9, 10, 12, EnemyClass.Fizz)]  // heal twice
        [TestCase(9, 9, 1, 2, 10, 2, EnemyClass.Dumb)]  // dont heal
        public void WhenScoreMultiplierReached_HealPlayer(
            float initHealth,
            float expectedHealth,
            int healAmount,
            int initScore,
            int scoreMultiplier,
            int enemyScore,
            EnemyClass shotClass
        )
        {
            // arrange
            var playerHealth = ScriptableObject.CreateInstance<FloatVariable>();
            playerHealth.Value = initHealth;
            
            var score = ScriptableObject.CreateInstance<IntVariable>();
            score.Value = initScore;
            
            var rules = ScriptableObject.CreateInstance<GameRules>();
            rules.HealthRules.HealPlayerOnScoreMultiplier = scoreMultiplier;
            rules.HealthRules.HealAmount = healAmount;
            
            var enemy = new Enemy(new Vector2(1, 0), enemyScore);
            var enemies = new List<Enemy>
            {
                enemy,
            };
            var gameController = TestsUtils.GetGameController(
                playerHealth: playerHealth,
                scoreVariable: score,
                rules: rules,
                enemies: enemies);

            // act
            gameController.PlayTurn(enemy, shotClass);

            // assert
            Assert.AreEqual(expectedHealth, playerHealth.Value);
        }
    }
}