﻿using System;
using System.Threading;
using System.Threading.Tasks;
using AsyncUtils;
using Core;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils.Attributes;
using Utils.Input;

namespace View
{
    public class InputController : MonoBehaviour
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private GameControllerView _gameController;
        
        [SerializeField] private Camera _camera;
        
        [SerializeField] private LayerMask _enemiesMask;

        [SerializeField, Tooltip("Bounds around an enemy to keep showing weapon selector, e.i. if the cursor leaves the" +
                                 "bounds area, then the weapon selector popup will hide.")]
        private Vector2 _cursorLeaveBounds;

        [SerializeField] private WeaponSelector _selectorPopupPrefab;
        
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private EnemyClass _shotClass;

        private CancellationTokenSource _cts;
        private readonly RaycastHit2D[] _raycastResultsBuffer = new RaycastHit2D[10];

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

        private void Start()
        {
            var ct = _cts.Token;
            StartGameLoop(ct);
        }

        private async void StartGameLoop(CancellationToken ct)
        {
            var pointerAction = InputActionUtils.GetPointAction();
            pointerAction.Enable();
            
            while (!ct.IsCancellationRequested)
            {
                // wait enter enemy
                var enemyView = await WaitEnterEnemy(pointerAction, ct);
                var enemyPosition = enemyView.transform.position;

                var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
                var linkedCt = linkedCts.Token;

                try
                {
                    // show weapon selector popup
                    var popupTask = Popups.ShowPopup(_selectorPopupPrefab, enemyPosition, linkedCt);
                    var enterDifferentEnemyTask = WaitEnterDifferentEnemy(enemyView, pointerAction, linkedCt);
                    var leaveEnemyTask = WaitCursorLeaveEnemy(pointerAction, enemyPosition, linkedCt);

                    var finishedTask = await Task.WhenAny(popupTask, enterDifferentEnemyTask, leaveEnemyTask);
                   
                    // cancel unfinished tasks
                    linkedCts.Cancel();
                    
                    if (finishedTask == popupTask)
                    {
                        var shotClass = await popupTask;
                        // play turn
                        await _gameController.PlayTurn(enemyView.EnemyData, shotClass);
                    }
                }
                finally
                {
                    linkedCts.Dispose();
                }
            }
            
            pointerAction.Disable();
            pointerAction.Dispose();
        }

        private async Task<EnemyView> WaitEnterEnemy(InputAction pointerAction, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                // get cursor position
                var cursorPosition = GetCursorPosition(pointerAction);
            
                // raycast to detect enemies
                var hitsCount = Physics2D.RaycastNonAlloc(
                    cursorPosition,
                    Vector2.zero,
                    _raycastResultsBuffer,
                    1,
                    _enemiesMask);
                
                if (hitsCount > 0)  // an enemy has been detected
                {
                    var enemyView = _raycastResultsBuffer[0].collider.GetComponent<EnemyView>();
                    return enemyView;
                }

                await Task.Yield();
            }

            return null;
        }

        private async Task<EnemyView> WaitEnterDifferentEnemy(EnemyView enemy, InputAction pointerAction, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                var enteredEnemy = await WaitEnterEnemy(pointerAction, ct);
                if (enteredEnemy != enemy)
                {
                    return enteredEnemy;
                }

                await Task.Yield();
            }

            return null;
        }

        private async Task WaitCursorLeaveEnemy(InputAction pointerAction, Vector2 enemyPosition, CancellationToken ct)
        {
            bool inside = true;
            while (inside && !ct.IsCancellationRequested)
            {
                // get cursor position
                var cursorPosition = GetCursorPosition(pointerAction);
                var delta = (Vector2) cursorPosition - enemyPosition;

                if (Mathf.Abs(delta.x) > _cursorLeaveBounds.x || Mathf.Abs(delta.y) > _cursorLeaveBounds.y)
                {
                    Debug.Log($"cursor {cursorPosition} | enemy {enemyPosition}");
                    inside = false;
                }

                await Task.Yield();
            }
        }

        private Vector3 GetCursorPosition(InputAction pointerAction)
        {
            var pointerScreenPosition = pointerAction.ReadValue<Vector2>();
            var pointerWorldPosition = _camera.ScreenToWorldPoint(pointerScreenPosition);
            return pointerWorldPosition;
        }
    }
}