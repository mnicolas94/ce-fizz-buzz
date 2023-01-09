using SaveSystem;
using UnityEngine;
using Utils.Attributes;

namespace Settings
{
    public class SettingsInitializer : MonoBehaviour
    {
        [SerializeField, AutoProperty(AutoPropertyMode.Asset)]
        private SettingsData _settingsData;

        private async void Start()
        {
            await _settingsData.LoadOrCreate();
            _settingsData.VolumeOn = _settingsData.VolumeOn;
        }
    }
}