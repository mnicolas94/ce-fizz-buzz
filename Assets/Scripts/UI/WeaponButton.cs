using System.Threading;
using System.Threading.Tasks;
using Core;
using UnityEngine;
using UnityEngine.UI;
using Utils.Attributes;
using View.EnemyClassAppearance;

namespace UI
{
    public class WeaponButton : MonoBehaviour
    {
        [SerializeField] private EnemyClass _weaponClass;
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        [SerializeField, AutoProperty(AutoPropertyMode.Asset)]
        private AppearanceSettings _appearanceSettings;

        public async Task<EnemyClass> WaitForPress(CancellationToken ct)
        {
            await AsyncUtils.Utils.WaitPressButtonAsync(_button, ct);
            return _weaponClass;
        }

        [ContextMenu("Update color")]
        private void UpdateColor()
        {
            _image.color = _appearanceSettings.GetAppearance(_weaponClass).Color;
        }
    }
}