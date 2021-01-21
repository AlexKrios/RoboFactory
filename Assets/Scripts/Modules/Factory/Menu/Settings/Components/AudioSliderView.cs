using Modules.General.Audio;
using Modules.General.Audio.Models;
using Modules.General.Save;
using Modules.General.Settings;
using Modules.General.Ui.Common.Menu;
using UnityEngine;
using Zenject;

namespace Modules.Factory.Menu.Settings.Components
{
    [AddComponentMenu("Scripts/Factory/Menu/Settings/Audio Slider View")]
    public class AudioSliderView : VolumeSlider
    {
        #region Zenject
        
        [Inject] private readonly ISettingsController _settingsController;
        [Inject] private readonly IAudioController _audioController;
        [Inject] private readonly ISaveController _saveController;

        #endregion
        
        #region Unity Methods

        private void Start()
        {
            Slider.value = _settingsController.AudioVolume;
        }
        
        #endregion

        protected override void ChangeValue(float value)
        {
            base.ChangeValue(value);
            _audioController.ChangeAudioVolume(value);
            _audioController.PlayAudio(AudioClipType.ButtonClick);
            
            _saveController.SaveSettings();
        }
    }
}
