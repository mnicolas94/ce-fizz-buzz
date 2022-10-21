using System.Collections.Generic;
using System.Linq;
using Core;
using Core.GameRules;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditorMode
{
    public class BounceSequenceTests
    {
        private static readonly List<Enemy> Enemies = new List<Enemy>
        {
            new Enemy(new Vector2(1, 0), 1),
            new Enemy(new Vector2(1, 1.1f), 4),
            new Enemy(new Vector2(1, 2), 7),
            new Enemy(new Vector2(1, 4), 8),
            new Enemy(new Vector2(0, 1.5f), 3),
            new Enemy(new Vector2(0.4f, 2), 9),
            new Enemy(new Vector2(1, 1.5f), 12),
            new Enemy(new Vector2(0.5f, 1), 5),
            new Enemy(new Vector2(1.5f, 1), 15),
        };
        
        
        [TestCase(1.2f, 0, 0, 1, 2)]
        [TestCase(2.1f, 0, 0, 1, 2, 3)]
        [TestCase(2.1f, 1, 1, 2, 0)]
        [TestCase(0.1f, 0, 0)]
        [TestCase(1f, 4, 4, 5, 6)]
        [TestCase(1.1f, 5, 5, 4, 6)]
        [TestCase(100f, 7, 7)]
        [TestCase(100f, 8, 8)]
        public void GetShotBounceSequence_OrderIsCorrect(
            float distanceRule,
            int shotIndex,
            params int [] sequenceIndices)
        {
            // arrange
            var rules = ScriptableObject.CreateInstance<GameRules>();
            rules.DistanceToBounceShot = distanceRule;
            var shootEnemy = Enemies[shotIndex];
            var expected = sequenceIndices.Select(index => Enemies[index]).ToList();

            // act
            var result = GameMechanics.GetShotBounceSequence(shootEnemy, Enemies, rules);

            // assert
            Assert.AreEqual(expected.Count, result.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                var exp = expected[i];
                var res = result[i];
                Assert.AreSame(exp, res);
            }
        }
    }
}