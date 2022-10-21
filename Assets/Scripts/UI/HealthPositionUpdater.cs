using BrunoMikoski.AnimationSequencer;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils.Attributes;
using Utils.Input;

namespace UI
{
    /// <summary>
    /// Position the health bar in different positions based on the cursor position
    /// </summary>
    public class HealthPositionUpdater : MonoBehaviour
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Scene)]
        private Camera _camera;

        [SerializeField] private AnimationSequencerController _showOnTopAnimation;
        [SerializeField] private AnimationSequencerController _showBellowAnimation;
        
        private InputAction _pointerAction;
        private float _previousFrameYSign;

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

            var yCursorSign = Mathf.Sign(pointerWorldPosition.y);
            
            if (yCursorSign != _previousFrameYSign)
            {
                var animation = yCursorSign >= 0 ? _showBellowAnimation : _showOnTopAnimation;
                animation.Play();
            }

            _previousFrameYSign = yCursorSign;
        }
    }
}