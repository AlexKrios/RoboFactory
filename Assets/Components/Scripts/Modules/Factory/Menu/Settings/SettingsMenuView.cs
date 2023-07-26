using System.Globalization;
using RoboFactory.Auth;
using RoboFactory.General.Audio;
using RoboFactory.General.Localization;
using RoboFactory.General.Scene;
using RoboFactory.General.Settings;
using RoboFactory.General.Ui.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu.Settings
{
    public class SettingsMenuView : PopupBase
    {
        private const int VolumeStep = 10;
        
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly AudioService _audioService;
        [Inject] private readonly AuthService _authService;
        [Inject] private readonly SceneService _sceneService;
        [Inject] private readonly SettingsService _settingsService;

        [SerializeField] private TMP_Text _title;
        [SerializeField] private Button _signOut;

        [Header("Music Slider Components")]
        [SerializeField] private Slider _musicSlider;
        [SerializeField] private TMP_Text _musicSliderText;
        
        [Header("Audio Slider Components")]
        [SerializeField] private Slider _audioSlider;
        [SerializeField] private TMP_Text _audioSliderText;
        
        [Header("Graphic Slider Components")]
        [SerializeField] private Slider _graphicSlider;
        [SerializeField] private TMP_Text _graphicSliderText;
        
        [Space]
        [SerializeField] private LanguageSectionView _language;

        protected override void Awake()
        {
            base.Awake();
            
            _musicSlider.onValueChanged.AddListener(ChangeMusicSliderValue);
            _audioSlider.onValueChanged.AddListener(ChangeAudioSliderValue);
            _graphicSlider.onValueChanged.AddListener(ChangeGraphicSliderValue);

            _language.OnClickEvent += OnLanguageClick;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            _musicSlider.onValueChanged.RemoveListener(ChangeMusicSliderValue);
            _audioSlider.onValueChanged.RemoveListener(ChangeAudioSliderValue);
            _graphicSlider.onValueChanged.RemoveListener(ChangeGraphicSliderValue);
            
            _language.OnClickEvent -= OnLanguageClick;
        }

        public override void Initialize()
        {
            base.Initialize();
            
            _musicSlider.value = _settingsService.MusicVolume;
            _audioSlider.value = _settingsService.AudioVolume;
            _graphicSlider.value = (float)_settingsService.Graphics;
            _language.SetData();
            
            if (!_authService.IsGooglePlayConnected())
                _signOut.onClick.AddListener(OnSignOutClick);
            else
                _signOut.gameObject.SetActive(false);
            
            _title.text = _localizationService.GetLanguageValue(LocalizationKeys.SettingsMenuTitleKey);
        }

        private async void OnSignOutClick()
        {
            _authService.SignOut();
            await _sceneService.LoadScene(SceneName.Auth);
        }
        
        private void ChangeMusicSliderValue(float value)
        {
            var musicValue = value * VolumeStep;
            _musicSliderText.text = musicValue.ToString(CultureInfo.CurrentCulture);
            _audioService.ChangeMusicVolume(value);
        }
        
        private void ChangeAudioSliderValue(float value)
        {
            var audioValue = value * VolumeStep;
            _audioSliderText.text = audioValue.ToString(CultureInfo.CurrentCulture);
            _audioService.ChangeAudioVolume(value);
        }
        
        private void ChangeGraphicSliderValue(float value)
        {
            var graphics = (GraphicsType) value;
            _graphicSliderText.text = graphics.ToString();
            _settingsService.SetGraphics(graphics);
        }
        
        private void OnLanguageClick()
        {
            _title.text = _localizationService.GetLanguageValue(LocalizationKeys.SettingsMenuTitleKey);
        }
    }
}
