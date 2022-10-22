using System.Collections.Generic;
using System.Linq;
using Core.TurnSteps;

namespace Core
{
    public class GameController
    {
        private GameContext _context;

        private int _lastScoreMultiplierHeal;

        public GameContext Context => _context;

        public GameController(GameContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Spawn and move enemies when game starts. Similar to PlayTurn() without the shooting step.
        /// </summary>
        public List<TurnStep> StartGame()
        {
            // initialize health to maximum
            _context.PlayerHealth.Value = _context.MaxHealth.Value;
            
            // initialize score to 0
            _context.Score.Value = 0;
            
            // remove enemies due to serialization
            _context.Enemies.Clear();

            // set initial difficulty
            ChangeDifficulty();
            
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
            var shotBounceSequence = GameMechanics.GetShotBounceSequence(shotTarget, _context.Enemies, _context.GameRules);

            // check if enemy has the same class as shot
            bool sameClass = shotTarget.CurrentClass == shotClass;

            if (sameClass)
            {
                // destroy enemies
                int totalScore = 0;
                foreach (var enemy in shotBounceSequence)
                {
                    _context.Enemies.Remove(enemy);
                    _context.Score.Value += enemy.Score;
                    totalScore += enemy.Score;
                }

                // heal player based on score
                var healAmount = ComputeHealAmountBasedOnScore();
                if (healAmount > 0)
                {
                    _context.PlayerHealth.Value += healAmount;
                    yield return new HealPlayerTurnStep(healAmount);
                }
                
                // change difficulty if applicable
                ChangeDifficulty();
                
                yield return new DestroyTurnStep(shotBounceSequence, shotClass);
                yield return new ScoreChangedTurnStep(totalScore);
            }
            else
            {
                // change enemies' classes
                GameMechanics.ChangeEnemiesClass(shotClass, shotBounceSequence, _context.GameRules.SpawnRules);
                yield return new ChangeClassTurnStep(shotBounceSequence, shotClass);
            }
        }

        /// <summary>
        /// Compute the healing amount based on reached score.
        /// </summary>
        /// <returns></returns>
        private int ComputeHealAmountBasedOnScore()
        {
            var difference = _context.Score.Value - _lastScoreMultiplierHeal;
            var multiplier = _context.GameRules.HealthRules.HealPlayerOnScoreMultiplier;
            int healAmount = difference / multiplier;
            _lastScoreMultiplierHeal = _context.Score.Value - _context.Score.Value % multiplier;

            return healAmount;
        }

        /// <summary>
        /// Change difficulty based on score.
        /// </summary>
        /// <returns></returns>
        private bool ChangeDifficulty()
        {
            var scoreVariable = _context.Score;
            var difficulties = new List<ScoreRule>(_context.DifficultyScoreMilestones);
            difficulties.Sort(
                (scoreRuleA, scoreRuleB) => scoreRuleA.Score.CompareTo(scoreRuleB.Score));

            var newRule = _context.GameRules;
            foreach (var (score, rules) in difficulties)
            {
                if (scoreVariable.Value >= score)
                {
                    newRule = rules;
                }
            }

            bool changed = _context.GameRules != newRule;
            _context.GameRules = newRule;
            
            return changed;
        }

        /// <summary>
        /// Move enemies and do damage to player for each enemy that gets close
        /// </summary>
        private IEnumerable<TurnStep> ExecuteEnemiesActions()
        {
            // spawn enemies
            var newEnemies = GameMechanics.SpawnEnemies(_context.GameRules.SpawnRules);
            foreach (var enemy in newEnemies)
            {
                _context.Enemies.Add(enemy);
            }

            yield return new SpawnTurnStep(newEnemies);
            
            // move enemies
            GameMechanics.MoveEnemies(_context.Enemies, _context.GameRules, out var attackingEnemies);
            var nonAttackingEnemies = _context.Enemies.Where(enemy => !attackingEnemies.Contains(enemy)).ToList();
            
            yield return new MoveTurnStep(nonAttackingEnemies);

            float totalDamage = 0;
            foreach (var attackingEnemy in attackingEnemies)
            {
                _context.Enemies.Remove(attackingEnemy);
                _context.PlayerHealth.Value -= _context.GameRules.HealthRules.DamagePerHit;
                totalDamage += _context.GameRules.HealthRules.DamagePerHit;
            }

            if (attackingEnemies.Count > 0)
            {
                yield return new DamagePlayerTurnStep(attackingEnemies, totalDamage);
            }
        }
    }
}