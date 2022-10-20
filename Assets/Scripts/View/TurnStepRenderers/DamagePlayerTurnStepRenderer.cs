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
    public class DamagePlayerTurnStepRenderer : TurnStepRenderer
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private EnemyViewPool _enemiesPool;
        
        public override string DisplayName => "TurnStepRenderer-DamagePlayer";
        
        public override void AddTweenToSequence(Sequence animationSequence)
        {
            var turnStep = _lastTurnData.FirstOrDefault(step => step is DamagePlayerTurnStep);
            if (turnStep is DamagePlayerTurnStep damagePlayerTurnStep)
            {
                var enemiesViews = damagePlayerTurnStep.Enemies.Select(enemy => _enemiesPool.GetView(enemy));
                Sequence sequence = DOTween.Sequence();
                foreach (var enemyView in enemiesViews)
                {
                    var attackAnimation = enemyView.AttackAnimation.GenerateSequence();
                    sequence.Join(attackAnimation);
                    attackAnimation.onComplete += () => _enemiesPool.RemoveView(enemyView.EnemyData);
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