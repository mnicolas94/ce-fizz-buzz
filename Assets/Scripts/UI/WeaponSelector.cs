using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AsyncUtils;
using BrunoMikoski.AnimationSequencer;
using Core;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class WeaponSelector : AsyncPopup<EnemyClass, (Vector3, Camera)>
    {
        [SerializeField] private RectTransform _popupTransform;
        [SerializeField] private List<WeaponButton> _weaponButtons;
        [SerializeField] private AnimationSequencerController _hideAnimation;

        public override async Task<EnemyClass> Show(CancellationToken ct)
        {
            try
            {
                var pressTasks = _weaponButtons.ConvertAll(button => button.WaitForPress(ct));
                var finishedTask = await Task.WhenAny(pressTasks);
                var weaponClass = await finishedTask;

                return weaponClass;
            }
            finally
            {
                _hideAnimation.Play();
            }
        }

        public override void Initialize((Vector3, Camera) positionCamera)
        {
            // check if buttons get out of screen
            var (position, cam) = positionCamera;
            var semiHeight = _popupTransform.sizeDelta.y / 2;
            var limit = cam.orthographicSize - semiHeight;
            if (position.y >= limit)
            {
                // rotate
                _popupTransform.rotation = Quaternion.Euler(0, 0, 180);
            }
            
            _popupTransform.position = position;
        }
    }
}