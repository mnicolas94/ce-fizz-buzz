using System;
using UnityEngine;

namespace View.EnemyClassAppearance
{
    [Serializable]
    public class ClassAppearance
    {
        [SerializeField] private Color _color;
        [SerializeField] private Sprite _enemySprite;

        public Color Color => _color;

        public Sprite EnemySprite => _enemySprite;
    }
}