using System.Collections.Generic;
using Core;
using NUnit.Framework;
using UnityEngine;

namespace Tests.EditorMode
{
    public class MoveMechanicTests
    {
        [Test]
        public void WhenMoveEnemies_PositionsChange_Test()
        {
            // arrange
            var enemies = new List<Enemy>
            {
                new Enemy(new Vector2(1, 1), 1),
                new Enemy(new Vector2(-2, 3), 1),
                new Enemy(new Vector2(2, -4), 1),
                new Enemy(new Vector2(-1, -1), 1),
            };
            var rules = GameRules.Create();
            var enemiesPositions = enemies.ConvertAll(enemy => (enemy, enemy.Position));
            
            // act
            GameMechanics.MoveEnemies(enemies, rules, out var enemiesDamage);
            
            // assert
            foreach (var (enemy, position) in enemiesPositions)
            {
                Assert.AreNotEqual(enemy.Position, position);
            }
        }
        
        [Test]
        public void WhenMoving_DontPassThroughPlayer_Test()
        {
            // arrange
            var enemy = new Enemy(new Vector2(1, 1), 1);
            var enemies = new List<Enemy>
            {
                enemy
            };
            var rules = GameRules.Create(moveDistancePerTurn: 10);
            
            // act
            GameMechanics.MoveEnemies(enemies, rules, out var enemiesDamage);
            
            // assert
            Assert.GreaterOrEqual(0, enemy.Position.x);
            Assert.GreaterOrEqual(0, enemy.Position.y);
        }
        
        [Test]
        public void WhenMoveEnemyIntoDamageZone_ReturnItAsEnemyThatDamaged_Test()
        {
            // arrange
            var enemy1 = new Enemy(new Vector2(1, 1), 1);
            var enemy2 = new Enemy(new Vector2(0, 3), 1);
            var enemy3 = new Enemy(new Vector2(2, 0), 1);
            var enemy4 = new Enemy(new Vector2(-1.5f, 0), 1);
            var enemies = new List<Enemy>
            {
                enemy1,
                enemy2,
                enemy3,
                enemy4,
            };
            var rules = GameRules.Create(distanceToDamagePlayer: 1);
            
            // act
            GameMechanics.MoveEnemies(enemies, rules, out var enemiesDamage);
            
            // assert
            Assert.Contains(enemy1, enemiesDamage);
            Assert.Contains(enemy4, enemiesDamage);
            Assert.IsFalse(enemiesDamage.Contains(enemy2));
            Assert.IsFalse(enemiesDamage.Contains(enemy3));
        }
    }
}