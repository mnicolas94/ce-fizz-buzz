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
    public class DestroyEnemiesSequence : TurnStepRenderer
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private EnemyViewPool _enemiesPool;
        
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private ShotSequenceAnimation _shotSequenceRenderer;
        
        public override string DisplayName => "TurnStepRenderer-DestroyEnemies";
        
        public override void AddTweenToSequence(Sequence animationSequence)
        {
            var turnStep = _lastTurnData.FirstOrDefault(step => step is DestroyTurnStep);
            if (turnStep is DestroyTurnStep destroyStep)
            {
                var enemiesViews = destroyStep.Enemies.Select(enemy => _enemiesPool.GetView(enemy)).ToList();
                var shotSequence = _shotSequenceRenderer.GetShotSequence(
                    enemiesViews,
                    destroyStep.ShotClass,
                    enemyView =>
                    {
                        var sequence = enemyView.DestroyAnimation.GenerateSequence();
                        sequence.onComplete += () => _enemiesPool.RemoveView(enemyView.EnemyData);
                        return sequence;
                    });
            
                if (FlowType == FlowType.Append)
                    animationSequence.Append(shotSequence);
                else
                    animationSequence.Join(shotSequence);
            }
        }

        public override void ResetToInitialState()
        {
            
        }
    }
}