using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils.Input;

namespace View
{
    public class PlayerRotationController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        private InputAction _pointerAction;

        private void Start()
        {
            _pointerAction = InputActionUtils.GetPointAction();
            _pointerAction.Enable();
        }

        private void OnEnable()
        {
            _pointerAction?.Enable();
        }

        private void OnDisable()
        {
            _pointerAction.Disable();
        }

        private void Update()
        {
            var pointerScreenPosition = _pointerAction.ReadValue<Vector2>();
            var pointerWorldPosition = (Vector2) _camera.ScreenToWorldPoint(pointerScreenPosition);
            // rotate to look cursor
            transform.up = pointerWorldPosition;
        }
    }
}