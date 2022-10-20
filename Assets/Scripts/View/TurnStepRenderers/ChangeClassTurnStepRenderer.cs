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
    public class ChangeClassTurnStepRenderer : TurnStepRenderer
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private EnemyViewPool _enemiesPool;
        
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private ShotSequenceAnimation _shotSequenceRenderer;
        
        public override string DisplayName => "TurnStepRenderer-ChangeEnemiesClass";
        
        public override void AddTweenToSequence(Sequence animationSequence)
        {
            var turnStep = _lastTurnData.FirstOrDefault(step => step is ChangeClassTurnStep);
            if (turnStep is ChangeClassTurnStep changeClassStep)
            {
                var enemiesViews = changeClassStep.Enemies.Select(enemy => _enemiesPool.GetView(enemy)).ToList();
                var shotSequence = _shotSequenceRenderer.GetShotSequence(
                    enemiesViews,
                    changeClassStep.ShotClass,
                    GetShotEnemySequence);
            
                if (FlowType == FlowType.Append)
                    animationSequence.Append(shotSequence);
                else
                    animationSequence.Join(shotSequence);
            }
        }

        private Sequence GetShotEnemySequence(EnemyView enemyView)
        {
            var sequence = enemyView.ChangeClassAnimation.GenerateSequence();
            return sequence;
        }

        public override void ResetToInitialState()
        {
            
        }
    }
}