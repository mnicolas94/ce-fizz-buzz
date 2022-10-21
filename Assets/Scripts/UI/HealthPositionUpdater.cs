using UnityEngine;
using UnityEngine.InputSystem;
using Utils.Attributes;
using Utils.Input;

namespace UI
{
    public class HealthPositionUpdater : MonoBehaviour
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private Camera _camera;

        [SerializeField] private RectTransform _healthTransform;

        [SerializeField] private Vector2 _topPosition;
        [SerializeField] private Vector2 _bottomPosition;
        
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

            var newPosition = pointerWorldPosition.y >= 0 ? _bottomPosition : _topPosition;
            _healthTransform.anchoredPosition = newPosition;
        }
    }
}