using Components.Scripts.Modules.General.Audio;
using Components.Scripts.Modules.General.Settings;
using Components.Scripts.Modules.General.Ui.Common.Menu;
using UnityEngine;
using Zenject;

namespace Components.Scripts.Modules.Factory.Menu.Settings.Components
{
    [AddComponentMenu("Scripts/Factory/Menu/Settings/Audio Slider View")]
    public class AudioSliderView : VolumeSlider
    {
        #region Zenject
        
        [Inject] private readonly SettingsManager _settingsController;
        [Inject] private readonly AudioManager _audioController;

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
        }
    }
}
