using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.GameRules;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Utils.Attributes;

namespace View
{
    public class GameControllerView : MonoBehaviour
    {
        [SerializeField] private FloatVariable _healthVariable;
        [SerializeField] private FloatConstant _maxHealth;
        [SerializeField] private IntVariable _scoreVariable;
        [SerializeField] private GameRules _gameRules;

        [SerializeField, AutoProperty(AutoPropertyMode.Scene)] private TurnRenderer _turnRenderer;
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)] private EnemyViewPool _enemiesPool;

        private GameController _gameController;

        [ContextMenu("Start game")]
        public async Task StartGame()
        {
            var game = new GameController(_healthVariable, _maxHealth, _scoreVariable, _gameRules);
            await StartGame(game);
        }
        
        public async Task StartGame(GameController game)
        {
            _enemiesPool.Clear();
            _gameController = game;
            
            var turnSteps = _gameController.StartGame();
            await _turnRenderer.RenderTurn(turnSteps);
        }

        public async Task PlayTurn(Enemy enemy, EnemyClass shotClass)
        {
            var turnSteps = _gameController.PlayTurn(enemy, shotClass);
            await _turnRenderer.RenderTurn(turnSteps);
        }
    }
}