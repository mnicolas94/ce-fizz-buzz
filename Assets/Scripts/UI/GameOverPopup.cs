﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AsyncUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace UI
{
    public class GameOverPopup : AsyncPopup
    {
        [SerializeField] private Button _restartButton;

        public override void Initialize()
        {
        }

        public override async Task Show(CancellationToken ct)
        {
            await AsyncUtils.Utils.WaitPressButtonAsync(_restartButton, ct);
        }
    }
}