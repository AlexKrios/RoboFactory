using System.Globalization;
using RoboFactory.Authentication;
using RoboFactory.General.Audio;
using RoboFactory.General.Localisation;
using RoboFactory.General.Scene;
using RoboFactory.General.Settings;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Settings
{
    [AddComponentMenu("Scripts/Factory/Menu/Settings/Settings Menu View")]
    public class SettingsMenuView : PopupBase
    {
        private const int VolumeStep = 10;
        
        #region Zenject

        [Inject] private readonly LocalisationManager _localisationController;
        [Inject] private readonly AudioManager _audioManager;
        [Inject] private readonly AuthenticationManager _authenticationManager;
        [Inject] private readonly SceneController _sceneController;
        [Inject] private readonly SettingsManager _settingsManager;

        #endregion

        #region Components
        
        [SerializeField] private TMP_Text title;
        [SerializeField] private Button signOut;

        [Header("Music Slider Components")]
        [SerializeField] private Slider musicSlider;
        [SerializeField] private TMP_Text musicSliderText;
        
        [Header("Audio Slider Components")]
        [SerializeField] private Slider audioSlider;
        [SerializeField] private TMP_Text audioSliderText;
        
        [Header("Graphic Slider Components")]
        [SerializeField] private Slider graphicSlider;
        [SerializeField] private TMP_Text graphicSliderText;
        
        [Space]
        [SerializeField] private LanguageSectionView language;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            
            musicSlider.onValueChanged.AddListener(ChangeMusicSliderValue);
            audioSlider.onValueChanged.AddListener(ChangeAudioSliderValue);
            graphicSlider.onValueChanged.AddListener(ChangeGraphicSliderValue);

            language.OnClickEvent += OnLanguageClick;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            musicSlider.onValueChanged.RemoveListener(ChangeMusicSliderValue);
            audioSlider.onValueChanged.RemoveListener(ChangeAudioSliderValue);
            graphicSlider.onValueChanged.RemoveListener(ChangeGraphicSliderValue);
            
            language.OnClickEvent -= OnLanguageClick;
        }

        #endregion

        public override void Initialize()
        {
            base.Initialize();
            
            musicSlider.value = _settingsManager.MusicVolume;
            audioSlider.value = _settingsManager.AudioVolume;
            graphicSlider.value = (float)_settingsManager.Graphics;
            language.SetData();
            
            if (!_authenticationManager.IsGooglePlayConnected())
                signOut.onClick.AddListener(OnSignOutClick);
            else
                signOut.gameObject.SetActive(false);
            
            title.text = _localisationController.GetLanguageValue(LocalisationKeys.SettingsMenuTitleKey);
        }

        private void OnSignOutClick()
        {
            _authenticationManager.SignOut();
            _sceneController.LoadScene(SceneName.Authentication);
        }
        
        private void ChangeMusicSliderValue(float value)
        {
            var musicValue = value * VolumeStep;
            musicSliderText.text = musicValue.ToString(CultureInfo.CurrentCulture);
            _audioManager.ChangeMusicVolume(value);
        }
        
        private void ChangeAudioSliderValue(float value)
        {
            var audioValue = value * VolumeStep;
            audioSliderText.text = audioValue.ToString(CultureInfo.CurrentCulture);
            _audioManager.ChangeAudioVolume(value);
        }
        
        private void ChangeGraphicSliderValue(float value)
        {
            var graphics = (GraphicsType) value;
            graphicSliderText.text = graphics.ToString();
            _settingsManager.SetGraphics(graphics);
        }
        
        private void OnLanguageClick()
        {
            title.text = _localisationController.GetLanguageValue(LocalisationKeys.SettingsMenuTitleKey);
        }
    }
}
