using System;
using BrunoMikoski.AnimationSequencer;
using UnityEngine;
using Utils.Attributes;

namespace View.TurnStepRenderers
{
    [Serializable]
    public abstract class TurnStepRenderer : AnimationStepBase
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Asset)]
        protected SerializableTurn _lastTurnData;
    }
}