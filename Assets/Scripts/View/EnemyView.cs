using System.Threading.Tasks;
using BrunoMikoski.AnimationSequencer;
using Core;
using DG.Tweening;
using UnityEngine;

namespace View
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private AnimationSequencerController _spawnAnimation;
        [SerializeField] private AnimationSequencerController _moveAnimation;

        private Enemy _enemyData;

        public Enemy EnemyData => _enemyData;

        public AnimationSequencerController SpawnAnimation => _spawnAnimation;

        public AnimationSequencerController MoveAnimation => _moveAnimation;

        public void SetEnemyData(Enemy enemy)
        {
            _enemyData = enemy;
        }
    }
}