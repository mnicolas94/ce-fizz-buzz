using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public static class GameMechanics
    {
        public static void MoveEnemies(IEnumerable<Enemy> enemies, GameRules gameRules, out List<Enemy> enemiesDamagePlayer)
        {
            enemiesDamagePlayer = new List<Enemy>();

            foreach (var enemy in enemies)
            {
                var distanceToPlayer = enemy.Position.magnitude;
                distanceToPlayer -= gameRules.MoveDistancePerTurn;
                distanceToPlayer = Mathf.Max(0, distanceToPlayer);  // clamp on zero
                var newPosition = enemy.Position.normalized * distanceToPlayer;
                enemy.Position = newPosition;

                if (distanceToPlayer < gameRules.DistanceToDamagePlayer)
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
        public static List<Enemy> GetShotBounceSequence(Enemy targetEnemy, IEnumerable<Enemy> enemies, GameRules gameRules)
        {
            var bounceSequence = new List<Enemy>{ targetEnemy };
            
            // get enemies of the same class
            var targetClass = targetEnemy.CurrentClass;
            var sameClassEnemies = enemies.Where(enemy => enemy.CurrentClass == targetClass).ToList();

            // loop to get the bounce sequence
            var currentEnemy = targetEnemy;
            bool closestIsInReach = true;
            while (closestIsInReach && sameClassEnemies.Count > 0)
            {
                var (closest, distance) = GetClosestEnemy(currentEnemy, sameClassEnemies);
                closestIsInReach = distance < gameRules.DistanceToBounceShot;
                
                if (closestIsInReach)
                {
                    bounceSequence.Add(closest);
                    sameClassEnemies.Remove(closest);
                }
            }

            return bounceSequence;
        }

        private static (Enemy, float) GetClosestEnemy(Enemy target, List<Enemy> neighbors)
        {
            var sqrDistances = neighbors.ConvertAll(neighbor => 
                (neighbor, Vector2.SqrMagnitude(target.Position - neighbors[0].Position)));
            
            sqrDistances.Sort((neighborATuple, neighborBTuple) => 
                neighborATuple.Item2.CompareTo(neighborBTuple.Item2));
            
            return sqrDistances[0];
        }
    }
}