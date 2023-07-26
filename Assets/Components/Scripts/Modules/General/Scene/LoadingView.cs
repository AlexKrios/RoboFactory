using DG.Tweening;
using RoboFactory.Auth;
using RoboFactory.General.Localization;
using RoboFactory.General.Services;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.General.Scene
{
    public class LoadingView : MonoBehaviour
    {
        [Inject] private readonly DiContainer _container;
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly SceneService _sceneService;
        [Inject] private readonly AuthService _authService;
        
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private Slider _progressSlider;

        private readonly CompositeDisposable _disposable = new();
        
        private void Awake()
        {
            var services = _container.ResolveAll<Service>();
            _progressSlider.maxValue = services.Count;
            
            _authService.AuthStatus.Subscribe(x =>
            {
                _progressText.gameObject.SetActive(x == AuthStatusEnum.Success);
                _progressSlider.gameObject.SetActive(x == AuthStatusEnum.Success);
            });
            
            _sceneService.ProgressText
                .Subscribe(x => _progressText.text = _localizationService.GetLanguageValue(x.ToString()))
                .AddTo(_disposable);
            
            _sceneService.ProgressMainValue
                .Subscribe(x => _progressSlider.DOValue(x, 0.1f).SetEase(Ease.OutCubic))
                .AddTo(_disposable);
        }

        private void OnDestroy()
        {
            _disposable.Dispose();
        }
    }
}