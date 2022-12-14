using System.Threading.Tasks;
using Core;
using UnityEngine;
using Utils.Attributes;

namespace View
{
    public class GameControllerView : MonoBehaviour
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Asset)] private GameContext _gameContext;
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)] private TurnRenderer _turnRenderer;
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)] private EnemyViewPool _enemiesPool;

        private GameController _gameController;

        public GameController GameController => _gameController;

        public GameContext GameContext => _gameContext;

        [ContextMenu("Start game")]
        public async void StartGame()
        {
            await StartGameAsync();
        }
        
        public async Task StartGameAsync()
        {
            var game = new GameController(_gameContext);
            await StartGameAsync(game);
        }
        
        private async Task StartGameAsync(GameController game)
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
        
        public async Task SkipTurn()
        {
            var turnSteps = _gameController.SkipTurn();
            await _turnRenderer.RenderTurn(turnSteps);
        }
    }
}