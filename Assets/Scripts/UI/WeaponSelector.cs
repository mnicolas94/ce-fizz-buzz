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
    public class WeaponSelector : AsyncPopup<EnemyClass, Vector3>
    {
        [SerializeField] private Transform _popupTransform;
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

        public override void Initialize(Vector3 position)
        {
            _popupTransform.position = position;
        }
    }
}