using SaveSystem;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "SettingsData", menuName = "Settings/SettingsData", order = 0)]
    public class SettingsData : ScriptableObject, IPersistentResetable
    {
        [SerializeField] private bool _volumeOn;

        public bool VolumeOn
        {
            get => _volumeOn;
            set
            {
                _volumeOn = value;
                AudioListener.volume = value ? 1 : 0;
                this.Save();
            }
        }

        public void ResetToDefault()
        {
            _volumeOn = true;
        }
    }
}