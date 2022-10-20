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
        [SerializeField] private IntVariable _scoreVariable;
        [SerializeField] private GameRules _gameRules;

        [SerializeField, AutoProperty(AutoPropertyMode.Scene)] private TurnRenderer _turnRenderer;

        private GameController _gameController;

        [ContextMenu("Start game")]
        public async Task StartGame()
        {
            _gameController = new GameController(_healthVariable, _scoreVariable, _gameRules);
            
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