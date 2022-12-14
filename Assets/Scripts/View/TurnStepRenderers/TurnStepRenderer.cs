using System;
using BrunoMikoski.AnimationSequencer;
using Core.Serializables;
using UnityEngine;
using Utils.Attributes;

namespace View.TurnStepRenderers
{
    /// <summary>
    /// An animation step* for rendering a gameplay turn step**.
    ///
    /// * see https://github.com/brunomikoski/Animation-Sequencer
    /// </summary>
    [Serializable]
    public abstract class TurnStepRenderer : AnimationStepBase
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Asset)]
        protected SerializedTurnAsset _lastTurnData;
    }
}