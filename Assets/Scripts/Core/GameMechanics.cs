using System.Collections.Generic;
using System.Linq;
using Core.GameRules;
using UnityEngine;
using Utils;
using Utils.Extensions;

namespace Core
{
    public static class GameMechanics
    {
        public static void MoveEnemies(IEnumerable<Enemy> enemies, GameRules.GameRules gameRules, out List<Enemy> enemiesDamagePlayer)
        {
            enemiesDamagePlayer = new List<Enemy>();

            foreach (var enemy in enemies)
            {
                var distanceToPlayer = enemy.Position.magnitude;
                distanceToPlayer -= gameRules.MoveDistancePerTurn;
                distanceToPlayer = Mathf.Max(0, distanceToPlayer);  // clamp on zero
                var newPosition = enemy.Position.normalized * distanceToPlayer;
                enemy.Position = newPosition;

                if (distanceToPlayer < gameRules.HealthRules.DistanceToDamagePlayer)
                {
                    enemiesDamagePlayer.Add(enemy);
                }
            }
        }

        /// <summary>
        /// Get a sequence of enemies resulting from the game bounce mechanic. See TODO add mechanic's doc to wiki
        /// </summary>
        /// <param name="targetEnemy"></param>
        /// <param name="enemies"></param>
        /// <param name="gameRules"></param>
        /// <returns></returns>
        public static List<Enemy> GetShotBounceSequence(Enemy targetEnemy, IEnumerable<Enemy> enemies, GameRules.GameRules gameRules)
        {
            var bounceSequence = new List<Enemy>{ targetEnemy };
            
            // get enemies of the same class
            var targetClass = targetEnemy.CurrentClass;
            var sameClassEnemies = enemies
                .Where(enemy =>
                {
                    bool sameClass = enemy.CurrentClass == targetClass;
                    bool differentToTarget = enemy != targetEnemy;
                    return sameClass && differentToTarget;
                })
                .ToList();

            // loop to get the bounce sequence
            var currentEnemy = targetEnemy;
            bool closestIsInReach = true;
            while (closestIsInReach && sameClassEnemies.Count > 0)
            {
                var (closest, sqrDistance) = GetClosestEnemy(currentEnemy, sameClassEnemies);
                var distance = Mathf.Sqrt(sqrDistance);
                closestIsInReach = distance < gameRules.DistanceToBounceShot;
                
                if (closestIsInReach)
                {
                    currentEnemy = closest;
                    bounceSequence.Add(closest);
                    sameClassEnemies.Remove(closest);
                }
            }

            return bounceSequence;
        }
        
        public static void ChangeEnemiesClass(EnemyClass newClass, List<Enemy> enemies, SpawnRules spawnRules)
        {
            var newClassValues =
                EnemyClassUtils.GetValuesFromClass(newClass, spawnRules.MinNumber, spawnRules.MaxNumber).ToList();
            foreach (var enemy in enemies)
            {
                var randomValue = newClassValues.GetRandom();
                enemy.ChangeClass(randomValue);
            }
        }

        public static List<Enemy> SpawnEnemies(SpawnRules spawnRules)
        {
            var enemiesSpawned = new List<Enemy>();
            var spawnCount = Random.Range(spawnRules.MinSpawnCount, spawnRules.MaxSpawnCount);

            // clamp to avoid negative values or above the number of circular sectors available for spawning
            spawnCount = Mathf.Clamp(spawnCount, 0, spawnRules.CircularSectors);

            var availableAngles = spawnRules.GetAvailableSpawnAngles();
            
            for (int i = 0; i < spawnCount; i++)
            {
                var angle = availableAngles.PopRandom();
                var distance = spawnRules.DistanceToSpawnEnemy;

                var position = MathUtils.FromPolar(distance, angle);
                
                var classValue = spawnRules.GetRandomClassValue();
                
                var enemy = new Enemy(position, classValue);
                enemiesSpawned.Add(enemy);
            }

            return enemiesSpawned;
        }

        private static (Enemy, float) GetClosestEnemy(Enemy target, List<Enemy> neighbors)
        {
            var sqrDistances = neighbors.ConvertAll(neighbor => 
                (neighbor, Vector2.SqrMagnitude(target.Position - neighbor.Position)));
            
            sqrDistances.Sort((neighborATuple, neighborBTuple) => 
                neighborATuple.Item2.CompareTo(neighborBTuple.Item2));
            
            return sqrDistances[0];
        }
    }
}