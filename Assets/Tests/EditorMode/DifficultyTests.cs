using System.Collections;
using System.Collections.Generic;
using Core;
using Core.GameRules;
using NUnit.Framework;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.EditorMode
{
    public class DifficultyTests
    {
        [TestCase(4, 10, 6, EnemyClass.Fizz)]
        [TestCase(567, 568, 7, EnemyClass.Dumb)]
        [TestCase(290, 300, 11, EnemyClass.Dumb)]
        public void WhenScoreMilestoneReached_ChangeDifficulty(
            int initScore,
            int scoreMilestone,
            int enemyScore,
            EnemyClass shotClass
        )
        {
            // arrange
            var score = ScriptableObject.CreateInstance<IntVariable>();
            score.Value = initScore;
            
            var initialRule = ScriptableObject.CreateInstance<GameRules>();
            var secondRule = ScriptableObject.CreateInstance<GameRules>();
            var difficulties = new List<ScoreRule>
            {
                new ScoreRule(scoreMilestone, secondRule)
            };
            
            var enemy = new Enemy(new Vector2(1, 0), enemyScore);
            var enemies = new List<Enemy>
            {
                enemy,
            };
            var gameController = TestsUtils.GetGameController(
                scoreVariable: score,
                rules: initialRule,
                difficulties: difficulties,
                enemies: enemies);

            // act
            gameController.PlayTurn(enemy, shotClass);

            // assert
            Assert.AreEqual(gameController.Context.GameRules, secondRule);
        }
    }
}