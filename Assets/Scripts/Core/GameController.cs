using System.Collections.Generic;
using System.Linq;
using Core.TurnSteps;
using UnityAtoms.BaseAtoms;

namespace Core
{
    public class GameController
    {
        private FloatVariable _playerHealth;
        private FloatConstant _maxHealth;
        private IntVariable _score;
        private IList<Enemy> _enemies;
        private GameRules.GameRules _gameRules;

        public GameController(FloatVariable playerHealth, FloatConstant maxHealth, IntVariable score, GameRules.GameRules gameRules)
            : this(playerHealth, maxHealth, score, gameRules, new List<Enemy>())
        {
        }
        
        public GameController(
            FloatVariable playerHealth,
            FloatConstant maxHealth,
            IntVariable score,
            GameRules.GameRules gameRules,
            List<Enemy> initialEnemies)
        {
            _playerHealth = playerHealth;
            _maxHealth = maxHealth;
            _score = score;
            _gameRules = gameRules;
            _enemies = initialEnemies;
        }

        /// <summary>
        /// Spawn and move enemies when game starts. Similar to PlayTurn() without the shooting step.
        /// </summary>
        public List<TurnStep> StartGame()
        {
            // initialize health to maximum
            _playerHealth.Value = _maxHealth.Value;
            
            return ExecuteEnemiesActions().ToList();
        }

        /// <summary>
        /// Execute a turn logic. 
        /// </summary>
        /// <param name="shotTarget"></param>
        /// <param name="shotClass"></param>
        public List<TurnStep> PlayTurn(Enemy shotTarget, EnemyClass shotClass)
        {
            var turnSteps = ExecutePlayerActions(shotTarget, shotClass);
            turnSteps = turnSteps.Concat(ExecuteEnemiesActions());

            return turnSteps.ToList();
        }

        /// <summary>
        /// Destroy or change class of enemies based on shot class.
        /// </summary>
        /// <param name="shotTarget"></param>
        /// <param name="shotClass"></param>
        private IEnumerable<TurnStep> ExecutePlayerActions(Enemy shotTarget, EnemyClass shotClass)
        {
            // get neighbour enemies sorted by shot bounce
            var shotBounceSequence = GameMechanics.GetShotBounceSequence(shotTarget, _enemies, _gameRules);

            // check if enemy has the same class as shot
            bool sameClass = shotTarget.CurrentClass == shotClass;

            if (sameClass)
            {
                // destroy enemies
                int totalScore = 0;
                foreach (var enemy in shotBounceSequence)
                {
                    _enemies.Remove(enemy);
                    _score.Value += enemy.Score;
                    totalScore += enemy.Score;
                    // TODO heal based on score
                }
                yield return new DestroyTurnStep(shotBounceSequence, shotClass);
                yield return new ScoreChangedTurnStep(totalScore);
            }
            else
            {
                // change enemies' classes
                GameMechanics.ChangeEnemiesClass(shotClass, shotBounceSequence, _gameRules.SpawnRules);
                yield return new ChangeClassTurnStep(shotBounceSequence, shotClass);
            }
        }

        /// <summary>
        /// Move enemies and do damage to player for each enemy that gets close
        /// </summary>
        private IEnumerable<TurnStep> ExecuteEnemiesActions()
        {
            // spawn enemies
            var newEnemies = GameMechanics.SpawnEnemies(_gameRules.SpawnRules);
            foreach (var enemy in newEnemies)
            {
                _enemies.Add(enemy);
            }

            yield return new SpawnTurnStep(newEnemies);
            
            // move enemies
            GameMechanics.MoveEnemies(_enemies, _gameRules, out var attackingEnemies);
            var nonAttackingEnemies = _enemies.Where(enemy => !attackingEnemies.Contains(enemy)).ToList();
            
            yield return new MoveTurnStep(nonAttackingEnemies);

            float totalDamage = 0;
            foreach (var attackingEnemy in attackingEnemies)
            {
                _enemies.Remove(attackingEnemy);
                _playerHealth.Value -= _gameRules.HealthRules.DamagePerHit;
                totalDamage += _gameRules.HealthRules.DamagePerHit;
            }

            if (attackingEnemies.Count > 0)
            {
                yield return new DamagePlayerTurnStep(attackingEnemies, totalDamage);
            }
        }
    }
}