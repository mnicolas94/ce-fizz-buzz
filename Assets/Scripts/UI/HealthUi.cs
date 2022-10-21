using System;
using System.Collections.Generic;
using System.Linq;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthUi : MonoBehaviour
    {
        [SerializeField] private FloatVariable _healthVariable;
        [SerializeField] private FloatConstant _maxHealth;
        [SerializeField] private Image _healthMask;
        [SerializeField] private Image _healthBar;
        [SerializeField] private List<RangeColor> _healthRangeColors;

        public void UpdateHealthUi()
        {
            var normalizedHealth = _healthVariable.Value / _maxHealth.Value;
            _healthMask.fillAmount = normalizedHealth;
            _healthBar.color = GetColorForHealth(normalizedHealth);
        }

        private Color GetColorForHealth(float normalizedHealth)
        {
            foreach (var (min, max, color) in _healthRangeColors)
            {
                if (normalizedHealth >= min && normalizedHealth <= max)
                {
                    return color;
                }
            }

            // default
            return _healthRangeColors.Last().Color;
        }
    }

    [Serializable]
    public class RangeColor
    {
        [SerializeField] public float Min;
        [SerializeField] public float Max;
        [SerializeField] public Color Color;

        public void Deconstruct(out float min, out float max, out Color color)
        {
            min = Min;
            max = Max;
            color = Color;
        }
    }
}