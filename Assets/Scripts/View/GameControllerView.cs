using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Core.GameRules;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Pool;

namespace View
{
    public class GameControllerView : MonoBehaviour
    {
        [SerializeField] private FloatVariable _healthVariable;
        [SerializeField] private IntVariable _scoreVariable;
        [SerializeField] private GameRules _gameRules;

        [SerializeField] private EnemyView _enemyViewPrefab;

        private GameController _gameController;
        private Dictionary<Enemy, EnemyView> _dataViewMap = new Dictionary<Enemy, EnemyView>();
        private ObjectPool<EnemyView> _enemiesPool;

        private void Start()
        {
            _enemiesPool = new ObjectPool<EnemyView>(
                () => Instantiate(_enemyViewPrefab, transform),
                view => view.gameObject.SetActive(true),
                view => view.gameObject.SetActive(false)
                );
        }
        
        

        public async Task StartGame()
        {
            _gameController = new GameController(_healthVariable, _scoreVariable, _gameRules);
            _gameController.StartGame();
        }

        public async Task PlayTurn()
        {
            
        }

        private void DrawTurn()
        {
            
        }
    }
}