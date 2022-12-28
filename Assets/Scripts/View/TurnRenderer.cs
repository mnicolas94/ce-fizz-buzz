using System.Collections.Generic;
using System.Threading.Tasks;
using BrunoMikoski.AnimationSequencer;
using Core.Serializables;
using Core.TurnSteps;
using DG.Tweening;
using UnityEngine;
using Utils.Attributes;

namespace View
{
    public class TurnRenderer : MonoBehaviour
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Asset)]
        private SerializedTurnAsset _lastTurnData;

        [SerializeField] private AnimationSequencerController _turnAnimation;

        private void Start()
        {
            _lastTurnData.Clear();
        }

        public async Task RenderTurn(List<TurnStep> turnSteps)
        {
            _lastTurnData.Clear();
            _lastTurnData.AddRange(turnSteps);
            _turnAnimation.Play();
            await _turnAnimation.PlayingSequence.AsyncWaitForCompletion();
        }
    }
}