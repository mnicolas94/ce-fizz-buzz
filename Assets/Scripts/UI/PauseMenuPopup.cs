﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AsyncUtils;
 using SaveSystem;
 using Settings;
 using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
 using Utils.Attributes;

 namespace UI
{
    public class PauseMenuPopup : AsyncPopup
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private Toggle _audioToggle;

        [SerializeField, AutoProperty(AutoPropertyMode.Asset)]
        private SettingsData _settingsData;

        private InputAction _resumeAction;

        public override void Initialize()
        {
            _quitButton.onClick.AddListener(Application.Quit);
            _resumeAction = Utils.Input.InputActionUtils.GetKeyAction(Key.Escape);
            _resumeAction.Enable();
            _audioToggle.isOn = _settingsData.VolumeOn;
        }

        public override async Task Show(CancellationToken ct)
        {
            // avoid listening to the same Escape key press that launched the popup and, hence, close immediately
            await Task.Yield();
            await Task.Yield();
            
            var resumeKeyTask = AsyncUtils.Utils.WaitForInputAction(_resumeAction, ct);
            var resumeButtonTask = AsyncUtils.Utils.WaitPressButtonAsync(_resumeButton, ct);
            await Task.WhenAny(resumeButtonTask, resumeKeyTask);
            await _settingsData.Save();
        }

        private void OnDisable()
        {
            _resumeAction?.Dispose();
        }
    }
}