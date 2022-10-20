using System;
using BrunoMikoski.AnimationSequencer;
using DG.Tweening;
using UnityEngine;

namespace View.TurnStepRenderers.DoTweenActions
{
    public class MoveEnemyAction : DOTweenActionBase
    {
        public override Type TargetComponentType => typeof(EnemyView);

        public override string DisplayName => "MoveEnemy";

        private EnemyView _target;
        private Vector2 _previousValue;
        
        protected override Tweener GenerateTween_Internal(GameObject target, float duration)
        {
            if (_target == null)
            {
                if (!target.TryGetComponent(out _target))
                {
                    return null;
                }
            }

            _previousValue = _target.transform.position;
            var tween = _target.transform.DOMove(_target.EnemyData.Position, duration);
            return tween;
        }

        public override void ResetToInitialState()
        {
            if (_target != null)
            {
                _target.transform.position = _previousValue;
            }
        }

    }
}