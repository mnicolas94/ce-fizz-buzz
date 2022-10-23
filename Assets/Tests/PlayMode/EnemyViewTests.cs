using System.Collections;
using Core;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using View;
using DG.Tweening;

namespace Tests.PlayMode
{
    public class EnemyViewTests
    {
        [OneTimeSetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("MainScene");
        }
        
        [UnityTest]
        public IEnumerator AfterStartGame_EnemiesViewsGetTheCorrectPosition()
        {
            // arrange
            var controllerView = Object.FindObjectOfType<GameControllerView>();
            
            // act
            yield return PlayModeTestsUtils.WaitForAsyncFunction(controllerView.StartGameAsync());
            
            // assert
            var enemiesPool = Object.FindObjectOfType<EnemyViewPool>();
            var enemiesViews = enemiesPool.GetViews();
            foreach (var enemyView in enemiesViews)
            {
                var expected = enemyView.EnemyData.Position;
                var result = (Vector2) enemyView.transform.position;
                Assert.AreEqual(expected, result);
            }
        }

        [UnityTest]
        public IEnumerator AfterMoveAnimation_EnemyViewGetTheCorrectPosition()
        {
            // arrange
            var controllerView = Object.FindObjectOfType<GameControllerView>();
            yield return PlayModeTestsUtils.WaitForAsyncFunction(controllerView.StartGameAsync());
            
            var enemiesPool = Object.FindObjectOfType<EnemyViewPool>();
            var enemiesViews = enemiesPool.GetViews();
            
            var enemy = new Enemy(new Vector2(4, 6.3f), 1);
            var enemyView = enemiesViews[0];
            enemyView.Initialize(enemy);

            // act
            yield return enemyView.MoveAnimation.PlayEnumerator();

            // assert
            var expectedX = enemy.Position.x;
            var expectedY = enemy.Position.y;
            var resultX = enemyView.transform.position.x;
            var resultY = enemyView.transform.position.y;
            
            Assert.AreEqual(expectedX, resultX);
            Assert.AreEqual(expectedY, resultY);
        }
        
        [UnityTest]
        public IEnumerator AfterDestroyEnemy_EnemyViewsCorrespondToEnemiesInGameContext()
        {
            // arrange
            var controllerView = Object.FindObjectOfType<GameControllerView>();
            yield return PlayModeTestsUtils.WaitForAsyncFunction(controllerView.StartGameAsync());

            var enemiesPool = Object.FindObjectOfType<EnemyViewPool>();
            var enemiesViews = enemiesPool.GetViews();
            
            var enemyView = enemiesViews[0];
            var enemy = enemyView.EnemyData;
            var enemyClass = enemy.CurrentClass;

            // act
            yield return PlayModeTestsUtils.WaitForAsyncFunction(controllerView.PlayTurn(enemy, enemyClass));
            enemiesViews = enemiesPool.GetViews();
            var enemies = controllerView.GameController.Context.Enemies;
            
            // assert
            Assert.AreEqual(enemies.Count, enemiesViews.Count);
            foreach (var view in enemiesViews)
            {
                Assert.Contains(view.EnemyData, enemies);
            }
        }
    }
}