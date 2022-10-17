using System.Collections.Generic;
using Core;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditorMode
{
    public class BounceSequenceTests
    {
        [Test]
        public void GetShotBounceSequence_Test()
        {
            // arrange
            var dumb1 = new Enemy(new Vector2(1, 0), 1);
            var dumb2 = new Enemy(new Vector2(1, 1), 4);
            var dumb3 = new Enemy(new Vector2(1, 2), 7);
            var dumb4 = new Enemy(new Vector2(4, 1), 8);
            var fizz = new Enemy(new Vector2(1, 1.5f), 3);
            var buzz = new Enemy(new Vector2(0.5f, 1), 5);
            var enemies = new List<Enemy>
            {
                dumb1, dumb2, dumb3, dumb4, fizz, buzz
            };
            var expected = new List<Enemy>{ dumb1, dumb2, dumb3 };

            // act
            var rules = GameRules.Create(distanceToBounceShot: 1.1f);
            var result = GameMechanics.GetShotBounceSequence(dumb1, enemies, rules);

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