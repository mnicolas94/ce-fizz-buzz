using BrunoMikoski.AnimationSequencer;
using Core;
using UnityEngine;

namespace View
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private AnimationSequencerController _spawnAnimation;
        [SerializeField] private AnimationSequencerController _moveAnimation;
        [SerializeField] private AnimationSequencerController _destroyAnimation;

        private Enemy _enemyData;

        public Enemy EnemyData => _enemyData;

        public AnimationSequencerController SpawnAnimation => _spawnAnimation;

        public AnimationSequencerController MoveAnimation => _moveAnimation;

        public AnimationSequencerController DestroyAnimation => _destroyAnimation;

        public void SetEnemyData(Enemy enemy)
        {
            _enemyData = enemy;
            gameObject.name = $"Enemy - {enemy.CurrentClass} ({enemy.Score})";
        }
    }
}