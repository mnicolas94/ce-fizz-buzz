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
        
        [SerializeField, Tooltip("Bounds offset w.r.t enemy.")]
        private Vector2 _cursorLeaveBoundsOffset;

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
                        else if (finishedTask == enterDifferentEnemyTask)
                        {
                            Debug.Log("enter differennt enemy");
                        }
                        else if (finishedTask == leaveEnemyTask)
                        {
                            Debug.Log("leave enemy task");
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