using RoboFactory.General.Localization;
using RoboFactory.General.Scene;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.General
{
    public class Loader : MonoBehaviour
    {
        [Inject] private readonly LocalizationService _localizationService;
        [Inject] private readonly SceneService _sceneService;
        
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private Slider _progressSlider;

        private readonly CompositeDisposable _disposable = new();

        private void Awake()
        {
            _sceneService.ProgressText
                .Subscribe(v => _progressText.text = _localizationService.GetLanguageValue(v.ToString()))
                .AddTo(_disposable);
            
            _sceneService.ProgressMainValue
                .Subscribe(v => _progressSlider.value = v)
                .AddTo(_disposable);
        }
    }
}