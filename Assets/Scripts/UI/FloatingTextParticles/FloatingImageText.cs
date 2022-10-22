using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.FloatingTextParticles
{
    public class FloatingImageText : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _countText;
        [SerializeField] private float _baseFontSize;
        [SerializeField] private Color _defaultColor;

        private Action _onSpawn;

        public void Spawn()
        {
            gameObject.SetActive(true);
            _image.enabled = false;
            _countText.enabled = false;
            _onSpawn?.Invoke();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetOnSpawn(Action onSpawnAction)
        {
            _onSpawn += onSpawnAction;
        }
        
        public void SetSprite(Sprite sprite)
        {
            _image.enabled = true;
            _image.sprite = sprite;
        }

        public void SetText(string text, Color color, float sizeScale)
        {
            _countText.enabled = true;
            _countText.text = text;
            _countText.fontSize = _baseFontSize * sizeScale;
            _countText.color = color;
        }
        
        public void SetText(string text, Color color)
        {
            SetText(text, color, 1);
        }
        
        public void SetText(string text, float sizeScale)
        {
            SetText(text, _defaultColor, sizeScale);
        }
        
        public void SetText(string text)
        {
            SetText(text, _defaultColor, 1);
        }
    }
}