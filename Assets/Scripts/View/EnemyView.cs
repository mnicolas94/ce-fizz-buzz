﻿using BrunoMikoski.AnimationSequencer;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Utils.Attributes;
using View.EnemyClassAppearance;

namespace View
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Asset)]
        private AppearanceSettings _appearanceSettings;
        [SerializeField,AutoProperty] private SpriteRenderer _spriteRenderer;
        [SerializeField,AutoProperty] private Transform _shadow;
        [SerializeField] private TextMeshProUGUI _numberText;
        
        [SerializeField] private AnimationSequencerController _spawnAnimation;
        [SerializeField] private AnimationSequencerController _moveAnimation;
        [SerializeField] private AnimationSequencerController _destroyAnimation;
        [SerializeField] private AnimationSequencerController _changeClassAnimation;
        [SerializeField] private AnimationSequencerController _attackAnimation;

        private Enemy _enemyData;

        public Enemy EnemyData => _enemyData;

        public AnimationSequencerController SpawnAnimation => _spawnAnimation;

        public AnimationSequencerController MoveAnimation => _moveAnimation;

        public AnimationSequencerController DestroyAnimation => _destroyAnimation;

        public AnimationSequencerController ChangeClassAnimation => _changeClassAnimation;

        public AnimationSequencerController AttackAnimation => _attackAnimation;

        public void Initialize(Enemy enemy)
        {
            _enemyData = enemy;
            UpdateAppearance();
            transform.localScale = Vector3.zero;
        }

        public void UpdateAppearance()
        {
            gameObject.name = $"Enemy - {_enemyData.CurrentClass} ({_enemyData.Score})";
            _spriteRenderer.sprite = _appearanceSettings.GetAppearance(_enemyData.CurrentClass).EnemySprite;
            LookAtCenter();
        }

        private void LookAtCenter()
        {
            var up = -_enemyData.Position;
            _spriteRenderer.transform.up = up;
            _shadow.transform.up = up;
        }
    }
}