using System;
using System.Collections.Generic;
using Core;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

namespace View.TurnStepRenderers
{
    public class ShotSequenceAnimation : MonoBehaviour
    {
        [SerializeField] private ShotRay _rayPrefab;
        [SerializeField] private float _timeBetweenRays;
        [SerializeField] private float _enemySequenceDelay;

        private ObjectPool<ShotRay> _rayPool;

        private void Start()
        {
            _rayPool = new ObjectPool<ShotRay>(
                () => Instantiate(_rayPrefab, transform),
                ray => ray.gameObject.SetActive(true),
                ray => ray.gameObject.SetActive(false)
                );
        }

        public Sequence GetShotSequence(
            List<EnemyView> enemies,
            EnemyClass shotClass,
            Func<EnemyView, Sequence> onRayReachedEnemy)
        {
            var sequence = DOTween.Sequence();
            var positions = enemies.ConvertAll(enemy => enemy.transform.position);
            positions.Insert(0, Vector3.zero);
            
            for (int i = 0; i < positions.Count - 1; i++)
            {
                var firstPosition = positions[i];
                var secondPosition = positions[i + 1];
                var ray = _rayPool.Get();
                ray.SetPositions(firstPosition, secondPosition);
                ray.SetShotClass(shotClass);
                
                var raySequence = ray.Animation.GenerateSequence();
                var enemyView = enemies[i];
                var enemySequence = onRayReachedEnemy(enemyView);
                
                var delay = _timeBetweenRays * i;
                sequence.Join(raySequence);
                sequence.Join(enemySequence);
                raySequence.SetDelay(delay);
                enemySequence.SetDelay(delay + _enemySequenceDelay);
            }

            return sequence;
        }
    }
}