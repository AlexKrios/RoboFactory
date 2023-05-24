using RoboFactory.General.Audio;
using RoboFactory.General.Settings;
using RoboFactory.General.Ui.Common;
using UnityEngine;
using Zenject;

namespace RoboFactory.Factory.Menu.Settings
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
