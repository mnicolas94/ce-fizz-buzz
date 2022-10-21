using BrunoMikoski.AnimationSequencer;
using Core;
using UnityEngine;
using Utils.Attributes;
using View.EnemyClassAppearance;

namespace View
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Asset)]
        private AppearanceSettings _appearanceSettings;
        [SerializeField,AutoProperty] private SpriteRenderer _spriteRenderer;
        
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
        }

        public void UpdateAppearance()
        {
            gameObject.name = $"Enemy - {_enemyData.CurrentClass} ({_enemyData.Score})";
            transform.up = -_enemyData.Position;
            _spriteRenderer.sprite = _appearanceSettings.GetAppearance(_enemyData.CurrentClass).EnemySprite;
        }
    }
}