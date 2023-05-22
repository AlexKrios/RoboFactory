using Components.Scripts.Modules.General.Audio;
using Components.Scripts.Modules.General.Settings;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Settings.Components
{
    [AddComponentMenu("Scripts/Factory/Menu/Settings/Music Slider View")]
    public class MusicSliderView : VolumeSlider
    {
        #region Zenject
        
        [Inject] private readonly SettingsManager _settingsController;
        [Inject] private readonly AudioManager _audioController;

        #endregion
        
        #region Unity Methods

        private void Start()
        {
            Slider.value = _settingsController.MusicVolume;
        }
        
        #endregion

        protected override void ChangeValue(float value)
        {
            base.ChangeValue(value);
            _audioController.ChangeMusicVolume(value);
        }
    }
}
