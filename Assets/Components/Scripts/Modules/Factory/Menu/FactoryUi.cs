using DG.Tweening;
using RoboFactory.Factory.Cameras;
using RoboFactory.Factory.Menu.Settings;
using RoboFactory.General.Audio;
using RoboFactory.General.Level;
using RoboFactory.General.Money;
using RoboFactory.General.Ui.Admin;
using RoboFactory.Utils;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace RoboFactory.Factory.Menu
{
    public class FactoryUi : MonoBehaviour
    {
        [Inject] private readonly AudioService _audioService;
        [Inject] private readonly MoneyService _moneyService;
        [Inject] private readonly ExperienceService _experienceService;
        [Inject] private readonly SettingsMenuFactory _settingsMenuFactory;
        [Inject] private readonly FactoryCameraController _factoryCameraController;

        [SerializeField] private DebugMenuView _debug;
        
        [Header("Money")]
        [SerializeField] private TMP_Text _moneyCount;
        
        [Header("Level")]
        [SerializeField] private TMP_Text _levelCount;
        [SerializeField] private Slider _levelSlider;
        
        [Header("Avatar")]
        [SerializeField] private Button _avatarButton;
        
        [Header("Settings")]
        [SerializeField] private Button _settingsButton;

        [Header("Elevator")]
        [SerializeField] private Button _floorUpButton;
        [SerializeField] private Button _floorDownButton;

        private readonly CompositeDisposable _disposable = new();
        
        private void Awake()
        {
            _moneyService.Money.Subscribe(SetMoneyData).AddTo(_disposable);
            _experienceService.OnExperienceSet += SetExperienceData;
            _experienceService.OnLevelSet += SetLevelData;
            
            _settingsButton.OnClickAsObservable().Subscribe(_ => OnSettingsClick()).AddTo(_disposable);
            _floorUpButton.OnClickAsObservable().Subscribe(_ => OnFloorUpClick()).AddTo(_disposable);
            _floorDownButton.OnClickAsObservable().Subscribe(_ => OnFloorDownClick()).AddTo(_disposable);
            _avatarButton.OnClickAsObservable().Subscribe(_ => OnAvatarClick()).AddTo(_disposable);

            _debug.gameObject.SetActive(true);
            
            _levelSlider.value = 0;
        }
        
        private void Start()
        {
            SetExperienceData();
            SetLevelData();
        }

        private void OnDestroy()
        {
            _experienceService.OnExperienceSet -= SetExperienceData;
            _experienceService.OnLevelSet -= SetLevelData;

            _disposable.Dispose();
        }

        private void SetMoneyData(int money)
        {
            _moneyCount.text = StringUtil.PriceFullFormat(money);
        }
        
        private void SetExperienceData()
        {
            var currentExperience = _experienceService.Experience - _experienceService.GetPreviousLevelCap();
            _levelSlider.maxValue = _experienceService.GetCurrentLevelCap() - _experienceService.GetPreviousLevelCap();
            if (currentExperience < _levelSlider.value)
            {
                var sequence = DOTween.Sequence();
                sequence
                    .Append(_levelSlider.DOValue(1, 0.1f).SetEase(Ease.OutCubic))
                    .AppendInterval(0.1f)
                    .Append(_levelSlider.DOValue(0, 0))
                    .Append(_levelSlider.DOValue(currentExperience, 0.5f).SetEase(Ease.OutCubic)); 
            }
            else
            {
                _levelSlider.DOValue(currentExperience, 1f).SetEase(Ease.OutCubic);
            }
        }

        private void SetLevelData()
        {
            _levelCount.text = _experienceService.Level.ToString();
        }
        
        private void OnAvatarClick()
        {
            SceneManager.LoadScene("UnitTest");
        }
        
        private void OnSettingsClick()
        {
            _audioService.PlayAudio(AudioClipType.ButtonClick);
            _settingsMenuFactory.CreateMenu();
        }

        private void OnFloorUpClick()
        {
            _audioService.PlayAudio(AudioClipType.ButtonClick);
            _factoryCameraController.Move(1);
        }

        private void OnFloorDownClick()
        {
            _audioService.PlayAudio(AudioClipType.ButtonClick);
            _factoryCameraController.Move(-1);
        }
    }
}