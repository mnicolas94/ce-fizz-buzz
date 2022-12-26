using System;
using System.Linq;
using BrunoMikoski.AnimationSequencer;
using Core.TurnSteps;
using DG.Tweening;
using UI;
using UI.FloatingTextParticles;
using UnityEngine;

namespace View.TurnStepRenderers
{
    [Serializable]
    public class HealPlayerTurnStepRenderer : TurnStepRenderer
    {
        [SerializeField] private HealthUi _healthUi;
        [SerializeField] private FloatingImageTextCreator _particleCreator;
        [SerializeField] private Color _textColor;
        
        public override string DisplayName => "TurnStepRenderer-HealPlayer";
        
        public override void AddTweenToSequence(Sequence animationSequence)
        {
            var turnStep = _lastTurnData.FirstOrDefault(step => step is HealPlayerTurnStep);
            if (turnStep is HealPlayerTurnStep healStep)
            {
                Sequence sequence = DOTween.Sequence();

                _healthUi.UpdateHealthUi(healStep.CurrentHealth);
                var particle = _particleCreator.Spawn();
                particle.transform.position = Vector3.zero;
                particle.SetText($"+{healStep.Heal}", _textColor);

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