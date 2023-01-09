using System;
using System.Reflection;
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
            PrintDatabase();
            await _settingsData.Load();
            _settingsData.VolumeOn = _settingsData.VolumeOn;
        }

        private void PrintDatabase()
        {
            var type = typeof(AssetGuidsDatabase);
            var bindingFlags = BindingFlags.NonPublic | BindingFlags.Instance;
            var member = type.GetField("_assetToGuid", bindingFlags);
            var value = member.GetValue(AssetGuidsDatabase.Instance) as AssetToGuidDictionary;
            foreach (var key in value.Keys)
            {
                Debug.Log(key.name);
            }
        }
    }
}