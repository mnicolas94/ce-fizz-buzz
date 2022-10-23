using System;
using BrunoMikoski.AnimationSequencer;
using Core;
using UnityEngine;
using Utils.Attributes;
using View.EnemyClassAppearance;

namespace View
{
    public class ShotRay : MonoBehaviour
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Asset)]
        private AppearanceSettings _appearanceSettings;
        [SerializeField] private Transform _ray;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        [SerializeField] private float _pivotPoint = 0.5f;
        [SerializeField] private float _heightScaleMultiplier = 1;

        [SerializeField] private AnimationSequencerController _animation;

        public AnimationSequencerController Animation => _animation;

        public void SetPositions(Vector2 init, Vector2 end)
        {
            // set object in the middle of positions
            var position = Vector2.Lerp(init, end, _pivotPoint);
            var rayTransform = _ray.transform;
            rayTransform.position = position;
            
            // rotate ray based on positions
            var dir = end - init;
            rayTransform.up = dir;
            
            // set vertical scale to make top and bottom bounds match positions
            var verticalScale = dir.magnitude * _heightScaleMultiplier;
            rayTransform.localScale = new Vector3(1, verticalScale, 1);
        }

        public void SetShotClass(EnemyClass shotClass)
        {
            var color = _appearanceSettings.GetAppearance(shotClass).Color;
            _spriteRenderer.color = color;
        }
    }
}