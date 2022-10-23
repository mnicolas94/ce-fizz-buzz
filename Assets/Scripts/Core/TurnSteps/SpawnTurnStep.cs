using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.TurnSteps
{
    [Serializable]
    public class SpawnTurnStep : TurnStep
    {
        [SerializeField] private List<EnemyPositionTuple> _enemiesPositions;

        public List<EnemyPositionTuple> EnemiesPositions => _enemiesPositions;

        public SpawnTurnStep(List<Enemy> enemies)
        {
            _enemiesPositions = enemies.ConvertAll(enemy => new EnemyPositionTuple(enemy));
        }
    }

    
    /// <summary>
    /// This class is used to preserve an enemy's position different to its current position, e.g a spawn position.
    /// </summary>
    [Serializable]
    public class EnemyPositionTuple
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private Vector2 _position;

        public Enemy Enemy => _enemy;

        public Vector2 Position => _position;

        public EnemyPositionTuple(Enemy enemy)
        {
            _enemy = enemy;
            _position = enemy.Position;
        }

        public void Deconstruct(out Enemy enemy, out Vector2 position)
        {
            enemy = _enemy;
            position = _position;
        }
    }
}