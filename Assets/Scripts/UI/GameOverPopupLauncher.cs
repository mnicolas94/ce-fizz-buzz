using System.Threading;
 using AsyncUtils;
 using UnityAtoms.BaseAtoms;
 using UnityEngine;
 using Utils.Attributes;
 using View;

 namespace UI
{
    public class GameOverPopupLauncher : MonoBehaviour
    {
        [SerializeField] private AsyncPopup _gameOverPopup;

        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private GameControllerView _gameController;

        [SerializeField] private FloatVariable _playerHealth;

        private CancellationTokenSource _cts;

        public void CheckGameOverCondition()
        {
            if (_playerHealth.Value <= 0)
            {
                ShowGameOverMenu();
            }
        }
        
        public void ShowGameOverMenu()
        {
            var ct = _cts.Token;
            ShowGameOverMenuAsync(ct);
        }

        private async void ShowGameOverMenuAsync(CancellationToken ct)
        {
            await Popups.ShowPopup(_gameOverPopup, ct);
            _gameController.StartGame();
        }

        private void OnEnable()
        {
            _cts = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }
            _cts.Dispose();
        }
    }
}