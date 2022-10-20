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
    public class DestroyEnemiesTurnStepRenderer : TurnStepRenderer
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private EnemyViewPool _enemiesPool;
        
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private ShotSequenceAnimation _shotSequenceRenderer;
        
        public override string DisplayName => "TurnStepRenderer-DestroyEnemies";
        
        public override void AddTweenToSequence(Sequence animationSequence)
        {
            var turnStep = _lastTurnData.FirstOrDefault(step => step is DestroyTurnStep);
            if (turnStep is DestroyTurnStep destroyTurnStep)
            {
                var enemiesViews = destroyTurnStep.Enemies.Select(enemy => _enemiesPool.GetView(enemy)).ToList();
                var shotSequence = _shotSequenceRenderer.GetShotSequence(
                    enemiesViews,
                    destroyTurnStep.ShotClass,
                    GetShotEnemySequence);
            
                if (FlowType == FlowType.Append)
                    animationSequence.Append(shotSequence);
                else
                    animationSequence.Join(shotSequence);
            }
        }

        private Sequence GetShotEnemySequence(EnemyView enemyView)
        {
            var sequence = enemyView.DestroyAnimation.GenerateSequence();
            sequence.onComplete += () => _enemiesPool.RemoveView(enemyView.EnemyData);
            return sequence;
        }

        public override void ResetToInitialState()
        {
            
        }
    }
}