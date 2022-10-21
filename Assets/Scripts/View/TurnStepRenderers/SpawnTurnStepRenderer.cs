using System;
using System.Linq;
using BrunoMikoski.AnimationSequencer;
using Core.TurnSteps;
using DG.Tweening;
using UnityEngine;
using Utils.Attributes;

namespace View.TurnStepRenderers
{
    [Serializable]
    public class SpawnTurnStepRenderer : TurnStepRenderer
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private EnemyViewPool _enemiesPool;
        
        public override string DisplayName => "TurnStepRenderer-SpawnEnemies";
        
        public override void AddTweenToSequence(Sequence animationSequence)
        {
            var turnStep = _lastTurnData.FirstOrDefault(step => step is SpawnTurnStep);
            if (turnStep is SpawnTurnStep spawnStep)
            {
                var enemiesViews = spawnStep.EnemiesPositions.Select(enemyPosition =>
                {
                    var (enemy, position) = enemyPosition;
                    var view = _enemiesPool.GetView(enemy);
                    view.transform.position = position;
                    return view;
                }).ToList();
                
                var animations = enemiesViews.Select(view => view.SpawnAnimation.GenerateSequence());
                var sequence = DOTween.Sequence();
                foreach (var animation in animations)
                {
                    sequence.Join(animation);
                }
            
                if (FlowType == FlowType.Append)
                    animationSequence.Append(sequence);
                else
                    animationSequence.Join(sequence);
            }
        }

        public override void ResetToInitialState()
        {
            
        }
    }
}