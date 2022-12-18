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
    public class MoveTurnStepRenderer : TurnStepRenderer
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private EnemyViewPool _enemiesPool;
        
        public override string DisplayName => "TurnStepRenderer-MoveEnemies";
        
        public override void AddTweenToSequence(Sequence animationSequence)
        {
            var turnStep = _lastTurnData.FirstOrDefault(step => step is MoveTurnStep);
            if (turnStep is MoveTurnStep moveStep)
            {
                var enemiesViews = moveStep.Enemies.Select(enemy => _enemiesPool.GetView(enemy));
                var animations = enemiesViews.Select(view => view.MoveAnimation.GenerateSequence());
                Sequence sequence = DOTween.Sequence();
                foreach (var animation in animations)
                {
                    sequence.Join(animation);
                }
            
                sequence.SetDelay(Delay);
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