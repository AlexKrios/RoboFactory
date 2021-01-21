using Modules.General.Audio;
using Modules.General.Save;
using Modules.General.Settings;
using Modules.General.Ui.Common.Menu;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Settings.Components
{
    [AddComponentMenu("Scripts/Factory/Menu/Settings/Music Slider View")]
    public class MusicSliderView : VolumeSlider
    {
        #region Zenject
        
        [Inject] private readonly ISettingsController _settingsController;
        [Inject] private readonly IAudioController _audioController;
        [Inject] private readonly ISaveController _saveController;
        
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
            
            _saveController.SaveSettings();
        }
    }
}
