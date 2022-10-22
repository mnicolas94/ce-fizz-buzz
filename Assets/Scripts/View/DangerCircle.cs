using BrunoMikoski.AnimationSequencer;
using Core;
using DG.Tweening;
using UnityEngine;
using Utils.Attributes;

namespace View
{
    public class DangerCircle : MonoBehaviour
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Asset)]
        private GameContext _context;
        
        [SerializeField] private float _edgeWidth;
        [SerializeField] private Transform _edge;
        [SerializeField] private Transform _fill;
        
        [SerializeField] private float _animationDuration;
        [SerializeField] private CustomEase _animationEase;

        [ContextMenu("Update")]
        public void UpdateCircle()
        {
            var rules = _context.GameRules;

            var tween = DOTween.To(() => _edge.localScale.x,
                diameter =>
                {
                    var fillDiameter = diameter - _edgeWidth;
                    _edge.localScale = new Vector3(diameter, diameter, diameter);
                    _fill.localScale = new Vector3(fillDiameter, fillDiameter, fillDiameter);
                },
                rules.GetDangerRadius() * 2,
                _animationDuration
            );

            tween.SetEase(_animationEase);

            tween.Play();
        }
    }
}