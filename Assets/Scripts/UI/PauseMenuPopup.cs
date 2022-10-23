﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AsyncUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class PauseMenuPopup : AsyncPopup
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _quitButton;

        private InputAction _resumeAction;

        public override void Initialize()
        {
            _quitButton.onClick.AddListener(Application.Quit);
            _resumeAction = Utils.Input.InputActionUtils.GetKeyAction(Key.Escape);
            _resumeAction.Enable();
        }

        public override async Task Show(CancellationToken ct)
        {
            // avoid listening to the same Escape key press that launched the popup and, hence, close immediately
            await Task.Yield();
            await Task.Yield();
            
            var resumeKeyTask = AsyncUtils.Utils.WaitForInputAction(_resumeAction, ct);
            var resumeButtonTask = AsyncUtils.Utils.WaitPressButtonAsync(_resumeButton, ct);
            await Task.WhenAny(resumeButtonTask, resumeKeyTask);
        }

        private void OnDisable()
        {
            _resumeAction?.Dispose();
        }
    }
}