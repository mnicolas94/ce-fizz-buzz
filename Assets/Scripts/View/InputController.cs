using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AsyncUtils;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
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
        
        [SerializeField, Tooltip("Bounds around an enemy to keep showing weapon selector, e.i. if the cursor leaves the" +
                                 "bounds area, then the weapon selector popup will hide.")]
        private Vector2 _cursorLeaveBounds;

        [SerializeField] private WeaponSelector _selectorPopupPrefab;

        private CancellationTokenSource _cts;
        private List<RaycastResult> _raycastResultsBuffer = new List<RaycastResult>();

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
            StartGameLoop();
        }

        public void StartGameLoop()
        {
            var ct = _cts.Token;
            StartGameLoop(ct);
        }

        private async void StartGameLoop(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
                var linkedCt = linkedCts.Token;
                try
                {
                    var inputTasks = new List<Task<Func<Task>>>
                    {
                        PlayTurnTask(linkedCt)
                    };
#if UNITY_EDITOR
                    // allow skip turn only in editor
                    inputTasks.Add(SkipTurnTask(linkedCt));
#endif
                    var finishedTask = await Task.WhenAny(inputTasks);
                    linkedCts.Cancel(); // cancel the unfinished task
                    var actionTaskProvider = await finishedTask;
                    var actionTask = actionTaskProvider();
                    await actionTask;
                }
                finally
                {
                    linkedCts.Dispose();
                }
            }
        }
        
        private async Task<Func<Task>> PlayTurnTask(CancellationToken ct)
        {
            var pointerAction = InputActionUtils.GetPointAction();
            pointerAction.Enable();

            try
            {
                while (!ct.IsCancellationRequested)
                {
                    // wait enter enemy
                    var enemyView = await WaitEnterEnemy(pointerAction, ct);
                    if (enemyView == null)
                    {
                        continue;
                    }
                    var enemyPosition = enemyView.transform.position;

                    var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
                    var linkedCt = linkedCts.Token;

                    try
                    {
                        // show weapon selector popup
                        var popupTask = Popups.ShowPopup(_selectorPopupPrefab, (enemyPosition, _camera), linkedCt);
                        var enterDifferentEnemyTask = WaitEnterDifferentEnemy(enemyView, pointerAction, linkedCt);
                        var leaveEnemyTask = WaitCursorLeaveEnemy(pointerAction, enemyPosition, linkedCt);

                        var finishedTask = await Task.WhenAny(popupTask, enterDifferentEnemyTask, leaveEnemyTask);
                   
                        // cancel unfinished tasks
                        linkedCts.Cancel();
                    
                        if (finishedTask == popupTask)
                        {
                            var shotClass = await popupTask;
                            // play turn
                            Task ActionTaskProvider() => _gameController.PlayTurn(enemyView.EnemyData, shotClass);
                            return ActionTaskProvider;
                        }
                    }
                    finally
                    {
                        linkedCts.Dispose();
                    }
                }
            }
            finally
            {
                pointerAction.Disable();
                pointerAction.Dispose();
            }

            return null;
        }
        
        private async Task<Func<Task>> SkipTurnTask(CancellationToken ct)
        {
            var keyAction = InputActionUtils.GetKeyAction(Key.Space);
            keyAction.Enable();

            try
            {
                await AsyncUtils.Utils.WaitForInputAction(keyAction, ct);  // wait for key press
                Task ActionTaskProvider() => _gameController.SkipTurn();
                return ActionTaskProvider;
            }
            finally
            {
                keyAction.Disable();
                keyAction.Dispose();
            }
        }

        private async Task<GameObject> WaitEnterSomething(InputAction pointerAction, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                // create pointer data
                var pointerEventData = new PointerEventData(EventSystem.current)
                {
                    position = pointerAction.ReadValue<Vector2>()
                };
                EventSystem.current.RaycastAll(pointerEventData, _raycastResultsBuffer);
                
                if (_raycastResultsBuffer.Count > 0)  // an enemy or ui component has been detected
                {
                    var rayCasted = _raycastResultsBuffer[0].gameObject;
                    return rayCasted;
                }

                await Task.Yield();
            }

            return null;
        }
        
        private async Task<EnemyView> WaitEnterEnemy(InputAction pointerAction, CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                var rayCasted = await WaitEnterSomething(pointerAction, ct);
                var enemyView = rayCasted.GetComponent<EnemyView>();
                if (enemyView != null)  // an enemy has been detected
                {
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
                var cursorPosition = GetCursorWorldPosition(pointerAction);
                var delta = (Vector2) cursorPosition - enemyPosition;

                if (Mathf.Abs(delta.x) > _cursorLeaveBounds.x || Mathf.Abs(delta.y) > _cursorLeaveBounds.y)
                {
                    inside = false;
                }

                await Task.Yield();
            }
        }

        private Vector3 GetCursorWorldPosition(InputAction pointerAction)
        {
            var pointerScreenPosition = pointerAction.ReadValue<Vector2>();
            var pointerWorldPosition = _camera.ScreenToWorldPoint(pointerScreenPosition);
            return pointerWorldPosition;
        }
    }
}