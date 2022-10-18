using System.Collections.Generic;
using UnityAtoms.BaseAtoms;

namespace Core
{
    public class GameController
    {
        private FloatVariable _playerHealth;
        private IntVariable _score;
        private IList<Enemy> _enemies;
        private GameRules.GameRules _gameRules;

        public GameController(FloatVariable playerHealth, IntVariable score, IList<Enemy> enemies, GameRules.GameRules gameRules)
        {
            _playerHealth = playerHealth;
            _score = score;
            _enemies = enemies;
            _gameRules = gameRules;
        }

        /// <summary>
        /// Spawn and move enemies when game starts. Similar to PlayTurn() without the shooting step.
        /// </summary>
        public void StartGame()
        {
            MoveEnemies();
        }

        /// <summary>
        /// Execute a turn logic. 
        /// </summary>
        /// <param name="shotTarget"></param>
        /// <param name="shotClass"></param>
        public void PlayTurn(Enemy shotTarget, EnemyClass shotClass)
        {
            ShootEnemies(shotTarget, shotClass);
            MoveEnemies();
        }

        /// <summary>
        /// Destroy or change class of enemies based on shot class.
        /// </summary>
        /// <param name="shotTarget"></param>
        /// <param name="shotClass"></param>
        private void ShootEnemies(Enemy shotTarget, EnemyClass shotClass)
        {
            // get neighbour enemies sorted by shot bounce
            var shotBounceSequence = GameMechanics.GetShotBounceSequence(shotTarget, _enemies, _gameRules);

            // check if enemy has the same class as shot
            bool sameClass = shotTarget.CurrentClass == shotClass;

            if (sameClass)
            {
                // destroy enemies
                foreach (var enemy in shotBounceSequence)
                {
                    _enemies.Remove(enemy);
                    _score.Value += enemy.Score;
                    // TODO heal based on score
                }
            }
            else
            {
                // change enemies' classes
                GameMechanics.ChangeEnemiesClass(shotClass, shotBounceSequence, _gameRules.SpawnRules);
            }
        }

        /// <summary>
        /// Move enemies and do damage to player for each enemy that gets close
        /// </summary>
        private void MoveEnemies()
        {
            // spawn enemies
            var newEnemies = GameMechanics.SpawnEnemies(_gameRules.SpawnRules);
            foreach (var enemy in newEnemies)
            {
                _enemies.Add(enemy);
            }
            
            // move enemies
            GameMechanics.MoveEnemies(_enemies, _gameRules, out var attackingEnemies);

            foreach (var attackingEnemy in attackingEnemies)
            {
                _enemies.Remove(attackingEnemy);
                _playerHealth.Value -= _gameRules.HealthRules.DamagePerHit;
            }
        }
    }
}