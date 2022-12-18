﻿using System.Collections.Generic;
using System.Threading;
 using System.Threading.Tasks;
 using AsyncUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UI
{
    public class PauseMenuLauncher : MonoBehaviour
    {
        [SerializeField] private AsyncPopup _pauseMenuPopup;
        [SerializeField] private InputAction _pauseAction;

        [SerializeField] private UnityEvent _onBeforePause;
        [SerializeField] private UnityEvent _onAfterPause;

        private CancellationTokenSource _cts;
        private bool _paused;

        public async void ShowPauseMenu()
        {
            await ShowPauseMenuAsync();
        }
        
        private async void ShowPauseMenuAsync(InputAction.CallbackContext ctx)
        {
            await ShowPauseMenuAsync();
        }

        private async Task ShowPauseMenuAsync()
        {
            if (!_paused)
            {
                _paused = true;
                _onBeforePause.Invoke();
                var ct = _cts.Token;
                await Popups.ShowPopup(_pauseMenuPopup, ct);
                _paused = false;
                _onAfterPause.Invoke();
            }
        }

        private void Start()
        {
            _pauseAction.Enable();
            _pauseAction.performed += ShowPauseMenuAsync;
        }

        private void OnEnable()
        {
            _cts = new CancellationTokenSource();
            _pauseAction?.Enable();
        }

        private void OnDisable()
        {
            if (!_cts.IsCancellationRequested)
            {
                _cts.Cancel();
            }
            _cts.Dispose();
            _pauseAction?.Disable();
        }
    }
}