using RoboFactory.Auth;
using RoboFactory.General.Localization;
using RoboFactory.General.Profile;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.General.Scene
{
    public class LoadingScreenView : MonoBehaviour
    {
        [Inject] private readonly CommonProfile _commonProfile;
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly SceneService _sceneService;
        
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private Slider _progressSlider;
        
        [SerializeField] private SignInView _signInForm;

        private readonly CompositeDisposable _disposable = new();
        
        private void Awake()
        {
            _commonProfile.AuthStatus.Subscribe(x =>
            {
                _progressText.gameObject.SetActive(x == AuthStatus.Success);
                _progressSlider.gameObject.SetActive(x == AuthStatus.Success);
                _signInForm.gameObject.SetActive(x != AuthStatus.Success);
            });
            
            _sceneService.ProgressText
                .Subscribe(v => _progressText.text = _localizationService.GetLanguageValue(v.ToString()))
                .AddTo(_disposable);
            
            _sceneService.ProgressMainValue
                .Subscribe(v => _progressSlider.value = v)
                .AddTo(_disposable);
        }
    }
}