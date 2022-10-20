using Core;
using UnityEngine;
using Utils.Attributes;

namespace View
{
    public class InputController : MonoBehaviour
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private GameControllerView _gameController;
        
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private EnemyClass _shotClass;

        [ContextMenu("Shoot")]
        public void Shoot()
        {
            _gameController.PlayTurn(_enemyView.EnemyData, _shotClass);
        }
    }
}